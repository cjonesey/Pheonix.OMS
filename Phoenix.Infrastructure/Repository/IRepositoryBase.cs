

using System.Linq.Expressions;

namespace Phoenix.Infrastructure
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity entity, int id);
        Task Delete(TEntity entity);
        Task DeleteByID(int id);
        Task<TEntity?> Get(int id);
        Task<List<TEntity>?> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int pageSize = 0, int page = 0);

        List<TEntity>? Get(Func<TEntity, bool> condition);
        Task<IEnumerable<TEntity>> GetAll();
		Task<List<TEntity>?> GetExpanded(
            Expression<Func<TEntity, bool>>? filter = null,
            Dictionary<string, byte>? orderBy = null,
            int pageSize = 0, int page = 0);
		Task Update(TEntity entity, int id);
    }
}