

namespace Pheonix.OMS.Domain.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}