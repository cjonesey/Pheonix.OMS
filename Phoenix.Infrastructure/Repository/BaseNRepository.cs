//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Phoenix.Infrastructure.Repository
//{
//    public class BaseNRepository<TEntity> :
//        IBaseNRepository<TEntity>
//            where TEntity : class
//    {
//        private readonly ILogger<BaseNRepository<TEntity>> _logger;
//        public BaseNRepository(
//            ILogger<BaseNRepository<TEntity>> logger)
//        {
//            _logger = logger;
//        }

//        #region public methods
//        public virtual async Task<IEnumerable<TEntity>> Get(
//            NHibernate.ISession session,
//            Expression<Func<TEntity, bool>>? filter,
//            List<Expression<Func<TEntity, object?>>>? includes,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
//            int pageNumber = 0,
//            int pageSize = 0,
//            CancellationToken cancellationToken = default)
//        {
//            //Use a connection from the connection pool 
//            var query = session.Query<TEntity>();
//            query = BuildQuery(filter, orderBy, pageNumber, pageSize, query);

//            if (includes != null && includes.Any())
//            {
//                includes.ForEach(x => query = query.Fetch(x));
//            }
//            return await query.ToListAsync(cancellationToken);
//        }

//        public async Task<IEnumerable<TOutput>> Get<TOutput>(ISession session,
//            Expression<Func<TEntity, bool>>? filter,
//            Func<ISession, IQueryable<TEntity>, IQueryable<TOutput>> applyQuery,
//            int pageNumber = 0,
//            int pageSize = 0,
//            CancellationToken cancellationToken = default) where TOutput : class
//        {
//            var query = session.Query<TEntity>();

//            if (filter != null)
//                query = query.Where(filter);

//            if (applyQuery == null)
//                throw new ArgumentNullException(nameof(applyQuery));

//            var modifiedQuery = applyQuery(session, query);

//            if (pageSize > 0 && pageNumber > 0)
//                modifiedQuery = modifiedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

//            return await modifiedQuery.ToListAsync(cancellationToken);
//        }

//        public async Task<TOutput> GetSingle<TOutput>(ISession session,
//            Expression<Func<TEntity, bool>>? filter,
//            Func<ISession, IQueryable<TEntity>, IQueryable<TOutput>> applyQuery,
//            CancellationToken cancellationToken = default) where TOutput : class
//        {
//            var query = session.Query<TEntity>();
//            if (filter != null)
//                query = query.Where(filter);

//            if (applyQuery == null)
//                throw new ArgumentNullException(nameof(applyQuery));

//            var modifiedQuery = applyQuery(session, query);

//            return await modifiedQuery.SingleAsync(cancellationToken);
//        }

//        public virtual async Task<IEnumerable<TEntity>> Get(
//            NHibernate.ISession session,
//            IQuerySpecification<TEntity> specification,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            query = GetQuery(query, specification); // Add in the specification
//            return await query.ToListAsync(cancellationToken);
//        }

//        public virtual async Task<IEnumerable<TOutput>> Get<TOutput>(
//            NHibernate.ISession session,
//            IQuerySpecificationWithSelect<TEntity, TOutput> specification,
//            CancellationToken cancellationToken = default)
//            where TOutput : class
//        {
//            var query = session.Query<TEntity>();
//            var modifiedQuery = GetQueryWithSelect<TOutput>(query, specification); // Add in the specification
//            return await modifiedQuery.ToListAsync(cancellationToken);
//        }

//        public virtual async Task<IEnumerable<TEntity>> All(NHibernate.ISession session,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            return await query.ToListAsync(cancellationToken);
//        }


//        public virtual async Task<int> Count(NHibernate.ISession session,
//            Expression<Func<TEntity, bool>>? filter = null,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            return await query.CountAsync(cancellationToken);
//        }

//        public virtual async Task<bool> Any(NHibernate.ISession session,
//            Expression<Func<TEntity, bool>>? filter = null,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            if (filter != null)
//                query = query.Where(filter);
//            return await query.AnyAsync(cancellationToken);
//        }

//        public virtual TEntity? GetSingle(NHibernate.ISession session,
//            object id)
//        {
//            return session.Load<TEntity>(id);
//        }

//        public virtual async Task<TEntity?> GetSingle(
//            NHibernate.ISession session,
//            Expression<Func<TEntity, bool>> filter,
//            Expression<Func<TEntity, object?>>? include,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            query = query.Where(filter);
//            if (include != null)
//                query = query.Fetch(include);
//            return await query.SingleOrDefaultAsync(cancellationToken);
//        }


//        public virtual async Task<TEntity?> GetSingle(
//            NHibernate.ISession session,
//            IQuerySpecification<TEntity> specification,
//            CancellationToken cancellationToken = default)
//        {
//            var query = session.Query<TEntity>();
//            query = GetQuery(query, specification); // Add in the specification
//            return await query.SingleAsync(cancellationToken);
//        }

//        public virtual async Task<TOutput?> GetSingle<TOutput>(
//            NHibernate.ISession session,
//            IQuerySpecificationWithSelect<TEntity, TOutput> specification,
//            CancellationToken cancellationToken = default) where TOutput : class
//        {
//            var query = session.Query<TEntity>();
//            var selectQuery = GetQueryWithSelect<TOutput>(query, specification); // Add in the specification
//            return await selectQuery.SingleAsync(cancellationToken);
//        }

//        public virtual async Task Insert(NHibernate.ISession session, TEntity entityToInsert)
//        {
//            List<ValidationResult> errors;
//            if (!ValidateEntity(entityToInsert, out errors))
//                throw new InvalidDataException("Data contains invalid details");
//            await session.SaveAsync(entityToInsert);
//        }

//        //public virtual void Update(NHibernate.ISession session,
//        //    Object id, TEntity updatedEntity,
//        //    Action<TEntity, TEntity> recordMapper)
//        //{
//        //    TEntity existingRecord = session.Load<TEntity>(id);
//        //    recordMapper(existingRecord, updatedEntity); //Updates the the fields that have been updated
//        //    session.SaveOrUpdate(existingRecord);
//        //}

//        public virtual async Task Update(NHibernate.ISession session,
//            TEntity updatedEntity)
//        {
//            await session.UpdateAsync(updatedEntity);
//        }

//        //public virtual void Update(NHibernate.ISession session, Object id, TEntity updatedEntity)
//        //{
//        //	TEntity existingRecord = session.Load<TEntity>(id);
//        //	if (existingRecord == null)
//        //		throw new InvalidDataException($"Entity {id} was not found");
//        //	session.SaveOrUpdate(existingRecord);
//        //}
//        public virtual async Task Delete(NHibernate.ISession session, TEntity updatedEntity)
//        {
//            await session.DeleteAsync(updatedEntity);
//        }

//        //public virtual async Task Delete(NHibernate.ISession session, object id)
//        //      {
//        //          await session.DeleteAsync(id);
//        //      }
//        #endregion

//        #region Database violations
//        #endregion

//        #region Transactions and Save
//        //When you do a Save in Entity Framework, it updates the database
//        //i.e. all updates, inserts and updates are committed in the sequence that they were transacted
//        //In NHibernate this is the same as flush / of the transaction commit
//        //We need to persist the session so we can save across entities 
//        #endregion

//        #region Private Methods

//        private bool ValidateEntity(TEntity entityToInsert, out List<ValidationResult> errors)
//        {
//            var validationContext = new ValidationContext(entityToInsert);
//            errors = new List<ValidationResult>();
//            return Validator.TryValidateObject(entityToInsert, validationContext, errors, true);
//        }

//        private static IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>>? filter,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
//            int pageNumber,
//            int pageSize, IQueryable<TEntity> query)
//        {
//            if (filter != null)
//                query = query.Where(filter);

//            if (orderBy != null)
//                query = orderBy(query);

//            if (pageSize > 0 && pageNumber > 0)
//                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

//            return query;
//        }

//        private IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, IQuerySpecification<TEntity> specification)
//        {
//            if (specification is null) throw new ArgumentNullException(nameof(specification));
//            return query
//                .ApplyInclude(specification.IncludeClause)
//                .ApplyFilter(specification.FilterClause)
//                .ApplyOrderBy(specification.OrderByClause)
//                .ApplyPaging(specification.PageClause, specification.PageSizeClause);
//        }

//        private IQueryable<TOutput> GetQueryWithSelect<TOutput>(
//            IQueryable<TEntity> query, IQuerySpecificationWithSelect<TEntity, TOutput> specification)
//            where TOutput : class
//        {
//            if (specification is null) throw new ArgumentNullException(nameof(specification));
//            if (specification.Select == null) throw new ArgumentNullException(nameof(specification));
//            return query
//                .ApplyInclude(specification.IncludeClause)
//                .ApplyFilter(specification.FilterClause)
//                .ApplyOrderBy(specification.OrderByClause)
//                .ApplyPaging(specification.PageClause, specification.PageSizeClause)
//                .Select(specification.SelectClause);
//        }

//        public async Task InsertMultiple(IStatelessSession session, List<TEntity> entitiesToInsert)
//        {
//            using (ITransaction transaction = session.BeginTransaction())
//            {
//                foreach (var entityRecord in entitiesToInsert)
//                {
//                    await session.InsertAsync(entityRecord);
//                }
//                await transaction.CommitAsync();
//            }
//        }

//        public async Task<IEnumerable<TOutput>> Get<TOutput>(ISession session, IQueryable<TOutput> queryable, CancellationToken cancellationToken = default) where TOutput : class
//        {
//            return await queryable.ToListAsync(cancellationToken);
//        }
//        #endregion
//    }
//}
