

using System.Linq.Expressions;

namespace Phoenix.Infrastructure
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity entity, int id);
        Task Delete(TEntity entity);
        Task DeleteByID(int id);
        Task<TEntity?> Get(int id);
        Task<List<TEntity>?> Get(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity, int id);
    }
}