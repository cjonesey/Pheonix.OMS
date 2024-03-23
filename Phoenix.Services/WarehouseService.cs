
namespace Phoenix.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger<WarehouseService> _logger;
        private readonly IRepositoryBase<Warehouse> _repository;

        public WarehouseService(ILogger<WarehouseService> logger,
            IRepositoryBase<Warehouse> repository)
        {
            _logger = logger;
            _repository = repository;

        }
        public async Task<List<WarehouseModel>?> GetAllWarehouses()
        {
            var warehouses = await _repository.GetAll();
            if (warehouses != null && warehouses.Any())
            {
                return warehouses.Select(w => new WarehouseModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Street1 = w.Street1,
                    Street2 = w.Street2,
                    City = w.City,
                    Postcode = w.Postcode,
                    CountryId = w.CountryId,
                    CountryCode = w.CountryCode,
                    ChangeCheck = w.ChangeCheck,
                    CreatedOn = w.CreatedOn,
                    ModifiedOn = w.ModifiedOn
                }).ToList();
            }
            return default;
        }

        public async Task<WarehouseModel?> GetWarehouseById(int id)
        {
            var warehouse = await _repository.Get(id);
            if (warehouse != null)
            {
                return new WarehouseModel
                {
                    Id = warehouse.Id,
                    Name = warehouse.Name,
                    Street1 = warehouse.Street1,
                    Street2 = warehouse.Street2,
                    City = warehouse.City,
                    County = warehouse.County,
                    Postcode = warehouse.Postcode,
                    CountryId = warehouse.CountryId,
                    CountryCode = warehouse.CountryCode,
                    ChangeCheck = warehouse.ChangeCheck,
                    CreatedOn = warehouse.CreatedOn,
                    ModifiedOn = warehouse.ModifiedOn
                };
            }
            return default;
        }

        public async Task<WarehouseModel?> AddWarehouse(WarehouseModel warehouseModel)
        {
            var warehouse = new Warehouse
            {
                Name = warehouseModel.Name,
                Street1 = warehouseModel.Street1,
                Street2 = warehouseModel.Street2,
                City = warehouseModel.City,
                County = warehouseModel.County,
                Postcode = warehouseModel.Postcode,
                CountryId = warehouseModel.CountryId,
                CountryCode = warehouseModel.CountryCode,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
            await _repository.Add(warehouse, warehouse.Id);
            return await GetWarehouseById(warehouse.Id);
        }

        public async Task<WarehouseModel?> UpdateWarehouse(WarehouseModel warehouseModel)
        {
            var warehouse = new Warehouse
            {
                Id = warehouseModel.Id,
                Name = warehouseModel.Name,
                Street1 = warehouseModel.Street1,
                Street2 = warehouseModel.Street2,
                City = warehouseModel.City,
                County = warehouseModel.County,
                Postcode = warehouseModel.Postcode,
                CountryId = warehouseModel.CountryId,
                CountryCode = warehouseModel.CountryCode,
                ChangeCheck = warehouseModel.ChangeCheck,
                CreatedOn = warehouseModel.CreatedOn,
                ModifiedOn = warehouseModel.ModifiedOn
            };
            await _repository.Update(warehouse, warehouse.Id);
            return await GetWarehouseById(warehouse.Id);
        }

        public async Task<WarehouseModel?> FindWarehouse(Dictionary<string,string> searchTerms, string genericSearch)
        {
            _repository.fin
            var warehouse = new Warehouse
            {
                Id = warehouseModel.Id,
                Name = warehouseModel.Name,
                Street1 = warehouseModel.Street1,
                Street2 = warehouseModel.Street2,
                City = warehouseModel.City,
                County = warehouseModel.County,
                Postcode = warehouseModel.Postcode,
                CountryId = warehouseModel.CountryId,
                CountryCode = warehouseModel.CountryCode,
                ChangeCheck = warehouseModel.ChangeCheck,
                CreatedOn = warehouseModel.CreatedOn,
                ModifiedOn = warehouseModel.ModifiedOn
            };
            await _repository.Update(warehouse, warehouse.Id);
            return await GetWarehouseById(warehouse.Id);
        }

        public async Task DeleteWarehouse(int warehouseID) => await _repository.DeleteByID(warehouseID);

    }
}
