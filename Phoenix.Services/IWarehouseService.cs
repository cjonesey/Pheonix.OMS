
namespace Phoenix.Services
{
    public interface IWarehouseService
    {
        Task<WarehouseModel?> AddWarehouse(WarehouseModel warehouseModel);
        Task DeleteWarehouse(int warehouseID);
        Task<List<WarehouseModel>?> FindWarehouse(
            Dictionary<string, string> searchTerms, 
            string genericSearch,
            string sortBy,
            int skip,
            int take);
        Task<List<WarehouseModel>?> GetAllWarehouses();
        Task<List<WarehouseModel>> GetAllWarehouses(int page = 0, int pageSize = 0);
        Task<WarehouseModel?> GetWarehouseById(int id);
        Task<WarehouseModel?> UpdateWarehouse(WarehouseModel warehouseModel);
    }
}