
namespace Phoenix.Services
{
    public interface IGenericService<T, U>
        where T : BaseEntity
        where U : BaseModel
    {
        Task<List<U>> GetAll(Func<T, U> MapResults);
        Task<U?> GetById(int id, Func<T, U> MapResults);
    }
}