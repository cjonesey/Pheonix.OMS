
namespace Phoenix.Services
{
    public interface IWarehouseService
    {
        Task<WarehouseModel?> AddWarehouse(WarehouseModel warehouseModel);
        Task DeleteWarehouse(int warehouseID);
        Task<List<WarehouseModel>?> FindWarehouse(Dictionary<string, string> searchTerms, string genericSearch);
        Task<List<WarehouseModel>?> GetAllWarehouses();
        Task<WarehouseModel?> GetWarehouseById(int id);
        Task<WarehouseModel?> UpdateWarehouse(WarehouseModel warehouseModel);
    }
}