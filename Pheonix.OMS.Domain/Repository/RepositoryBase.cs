using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        private readonly IDbContextFactory<PhoenixDBContext> _contextFactory;
        public RepositoryBase(IDbContextFactory<PhoenixDBContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        public virtual void Add(TEntity entity)
        {
            throw new NotImplementedException();
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
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }
        

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
