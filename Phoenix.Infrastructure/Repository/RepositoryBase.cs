using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Phoenix.Shared;
using System.IO;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Phoenix.Infrastructure
{
	public class RepositoryBase<TEntity> : 
        IDisposable,
        IRepositoryBase<TEntity> where TEntity : class
    {

        private readonly IDbContextFactory<PhoenixDBContext> _contextFactory;
        private readonly PhoenixDBContext _context;
        public RepositoryBase(IDbContextFactory<PhoenixDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public async Task Add(TEntity entity, int id)
        {
            var dbEntry = await _context.Set<TEntity>().FindAsync(id);
            if (dbEntry == null)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }


        public virtual async Task DeleteByID(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                throw new InvalidDataException("Record not found");
            await Delete(entity);
        }

        public async Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity?> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>?> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int pageSize = 0,
            int page = 0)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
                query = query.Where(filter);

            //Need to order the query by passing a predicate
            if (orderBy != null)
                query = orderBy(query);
            var eachItem = Expression.Parameter(typeof(TEntity), "item");
            var propertyToOrderByExpression = Expression.Property(eachItem, "PaymentDays");

            if (pageSize != 0)
                query = query.Skip(page).Take(pageSize);
            return await query.ToListAsync();
        }


		public virtual async Task<List<TEntity>?> GetUsingGenericSearch(
			List<(string key, string value, Type fieldType, BaseValues.SearchType searchType)>? entitySearchTerms = null,
			Dictionary<string, byte>? orderBy = null,
			int pageSize = 0,
	        int page = 0)
		{
			var predicate = PredicateBuilder.True<TEntity>();
			Expression<Func<TEntity, bool>>? condition = default;

            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (entitySearchTerms != null && entitySearchTerms.Any())
            {
                foreach (var searchRecord in entitySearchTerms)
                {
                    try
                    {
                        Type fieldType = searchRecord.fieldType;
                        if (searchRecord.value.Contains("..") && searchRecord.searchType == BaseValues.SearchType.Equals
                            && searchRecord.fieldType != typeof(string))
                        {
                            var values = searchRecord.value.Split("..");
                            if (values.Length >= 0)
                            {
                                condition = PredicateGenericHelper.CreateExpressionDynamicCall<TEntity>(
                                    searchRecord.key,
                                    values[0],
                                    BaseValues.SearchType.GreaterThanOrEqual,
                                    searchRecord.fieldType);
                                predicate = predicate.And(condition!);
                            }
                            if (values.Length >= 1)
                            {
                                condition = PredicateGenericHelper.CreateExpressionDynamicCall<TEntity>(
                                    searchRecord.key,
                                    values[1],
                                    BaseValues.SearchType.LessThanOrEqual,
                                    searchRecord.fieldType);
                                predicate = predicate.And(condition!);
                            }
                        }
                        else if (searchRecord.value.Contains('|') && searchRecord.searchType == BaseValues.SearchType.Equals)
                        {
                            condition = PredicateGenericHelper.CreateExpressionCallFromList<TEntity>(searchRecord.key, searchRecord.value, searchRecord.fieldType);
                            predicate = predicate.And(condition!);
                        }
                        else
                        {
                            condition = PredicateGenericHelper.CreateExpressionDynamicCall<TEntity>(
                                searchRecord.key,
                                searchRecord.value,
                                searchRecord.searchType,
                                searchRecord.fieldType);
                            predicate = predicate.And(condition!);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                query = query.Where(predicate);
			}


			//Need to order the query by passing a predicate
			if (orderBy != null && orderBy.Any())
            {
                foreach (var field in orderBy.Where(x => x.Value != 0))
                {
					query = query.OrderByTerm<TEntity>(field.Key, field.Value == 2 ? true : false);
				}

			}
			if (pageSize != 0)
				query = query.Skip(page).Take(pageSize);
			return await query.ToListAsync();
		}


		public List<TEntity>? Get(
            Func<TEntity, bool> condition)
        {
            return _context.Set<TEntity>().Where(condition).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        

        public async Task Update(TEntity entity, int id)
        {
            var dbEntry = await _context.Set<TEntity>().FindAsync(id);
            if (dbEntry != null)
            {
                var differences = ObjectHelpers.UpdateComparibleObject<TEntity>(dbEntry, entity);
                _context.Entry(dbEntry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

    }
}
