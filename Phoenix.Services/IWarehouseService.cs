
namespace Phoenix.Services
{
    public interface IWarehouseService
    {
        Task<WarehouseModel?> AddWarehouse(WarehouseModel warehouseModel);
        Task<List<WarehouseModel>?> GetAllWarehouses();
        Task<WarehouseModel?> GetWarehouseById(int id);
        Task<WarehouseModel?> UpdateWarehouse(WarehouseModel warehouseModel);
    }
}