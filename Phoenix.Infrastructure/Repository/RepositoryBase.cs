using Microsoft.EntityFrameworkCore;
using Phoenix.Shared;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;

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

        public virtual void Delete(TEntity entity)
        {
            throw new NotImplementedException();
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
            Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
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
