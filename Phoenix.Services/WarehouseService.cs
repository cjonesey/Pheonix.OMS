
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Phoenix.Models.Shared;
using Phoenix.Services.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Phoenix.Domain;

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


        public async Task<List<WarehouseModel>> GetAllWarehouses(int page = 0, int pageSize = 0)
        {
            List<WarehouseModel> warehousesModel = new();
            var warehouses = await _repository.Get(null, null, pageSize, page);
            if (warehouses != null && warehouses.Any())
            {
                warehouses.ToList().ForEach(warehouseModel => warehousesModel.Add(MapWarehouse(warehouseModel)));
                return warehousesModel;
            }
            return new List<WarehouseModel>();
        }


        public async Task<List<WarehouseModel>?> GetAllWarehouses()
        {
            List<WarehouseModel> warehousesModel = new();
            var warehouses = await _repository.GetAll();
            if (warehouses != null && warehouses.Any())
            {
                warehouses.ToList().ForEach(warehouseModel => warehousesModel.Add(MapWarehouse(warehouseModel)));
                return warehousesModel;
            }
            return default;
        }

        public async Task<WarehouseModel?> GetWarehouseById(int id)
        {
            var warehouse = await _repository.Get(id);
            if (warehouse != null)
            {
                return MapWarehouse(warehouse);
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

        public async Task<List<WarehouseModel>> FindWarehouse(Dictionary<string,string> searchTerms, 
            string genericSearch,
            string sortBy,
            int skip = 0,
            int take = 0)
        {
            var predicate = PredicateBuilder.True<Warehouse>();
            var warehouseProps = typeof(Warehouse).GetProperties();
            var warehouseSearchProps = typeof(WarehouseModel).GetProperties();
            List<Warehouse>? warehouses = new();
            List<WarehouseModel> warehousesModel = new();
            List<PropertyInfo> differences = new List<PropertyInfo>();
            Expression<Func<Warehouse, bool>> condition = default;
            bool predicateApplied = false;

            try
            {
                if (searchTerms != null && searchTerms.Any())
                {
                    foreach (var (key, value) in searchTerms
                        .Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
                    {
                        //Check that the value is in the warehouse entity
                        PropertyInfo? prop = warehouseProps.Where(x => x.Name == key).FirstOrDefault();
                        if (prop == null)
                        {
                            throw new Exception("Property does not exist");
                        }

                        PropertyInfo? propSearch1 = warehouseSearchProps.Where(x => x.Name == key).FirstOrDefault();
                        BaseValues.SearchType matchType = PredicateGenericHelper.GetSearchTypeForObject(propSearch1);

                        //Check whether the value contains the | character - only works for equals
                        if (value.Contains('|') && matchType == BaseValues.SearchType.Equals)
                        {
                            condition = PredicateGenericHelper.CreateExpressionCallFromList(key, value, prop);
                        }
                        else
                        {
                            PropertyInfo? propSearch = warehouseSearchProps.Where(x => x.Name == key).FirstOrDefault();
                            condition = PredicateGenericHelper.CreateExpressionCall<Warehouse>(
                                key,
                                value,
                                PredicateGenericHelper.GetMethod(prop.PropertyType, matchType),
                                prop.PropertyType);
                        }
                        predicate = predicate.And(condition!);
                        predicateApplied = true;
                    }
                }
                if (!string.IsNullOrEmpty(genericSearch))
                {
                    predicate = predicate.And(x => x.Name.Contains(genericSearch));
                    predicateApplied = true;
                }

                if (take == 0) { take = 20; }

                List<Warehouse>? result = null;
                if (predicateApplied)
                {
                    result  = await _repository.Get(predicate, x => x.OrderByDescending(x => x.Id), take, skip);
                }
                else
                {
                    result = await _repository.Get(null, null, take, skip);
                }

                if (result == null)
                    result = new List<Warehouse>();

                result.ForEach(warehouseModel => warehousesModel.Add(MapWarehouse(warehouseModel)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FindWarehouse");
            }
            return warehousesModel;
        }



        public async Task DeleteWarehouse(int warehouseID) => await _repository.DeleteByID(warehouseID);

        private static WarehouseModel MapWarehouse(Warehouse warehouse) => new WarehouseModel
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
}
