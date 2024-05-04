using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Services
{
    /// <summary>
    /// Basic Crud operation on PaymentServiceModel
    /// </summary>
    public class GenericService<T, U> : IGenericService<T, U> where T : BaseEntity
        where U : BaseModel
    {
        public ILogger<GenericService<T, U>> _logger;
        private readonly IRepositoryBase<T> _repository;

        public GenericService(ILogger<GenericService<T, U>> logger,
            IRepositoryBase<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<List<U>> GetAll(Func<T, U> MapResults)
        {
            List<U> outValues = new List<U>();
            var values = await _repository.GetAll();
            if (values != null && values.Any())
            {
                values.ToList().ForEach(x => outValues.Add(MapResults(x)));
            }
            return outValues;
        }

        public async Task<U?> GetById(int id, Func<T, U> MapResults)
        {
            var model = await _repository.Get(id);
            if (model != null)
            {
                return MapResults(model);
            }
            return default(U);
        }

        //public async Task<CountryModel?> AddCountry(CountryModel countryModel)
        //{
        //    var country = new Country
        //    {
        //        Code = countryModel.Code,
        //        ISOCode2 = countryModel.ISOCode2,
        //        Name = countryModel.Name,
        //        ModifiedOn = DateTime.Now,
        //        CreatedOn = DateTime.Now
        //    };
        //    await _repository.Add(country, country.Id);
        //    return await GetCountryById(country.Id);
        //}

        //public async Task<CountryModel?> UpdateCountry(CountryModel countryModel)
        //{
        //    var country = new Country
        //    {
        //        Id = countryModel.Id,
        //        Code = countryModel.Code,
        //        ISOCode2 = countryModel.ISOCode2,
        //        Name = countryModel.Name,
        //        CreatedOn = countryModel.CreatedOn,
        //        ModifiedOn = DateTime.Now,
        //        ChangeCheck = countryModel.ChangeCheck
        //    };
        //    await _repository.Update(country, country.Id);
        //    return await GetCountryById(country.Id);
        //}

        //public async Task<List<CountryModel>?> FindCountry(Dictionary<string, string> searchTerms, string genericSearch, string sortBy, int skip, int take)
        //{
        //    var predicate = PredicateBuilder.True<Country>();
        //    var countryProps = typeof(Country).GetProperties();
        //    var countrySearchProps = typeof(CountryModel).GetProperties();
        //    List<Country>? countries = new();
        //    List<CountryModel> countryModel = new();
        //    List<PropertyInfo> differences = new List<PropertyInfo>();
        //    Expression<Func<Country, bool>> condition = default;
        //    bool predicateApplied = false;

        //    try
        //    {
        //        if (searchTerms != null && searchTerms.Any())
        //        {
        //            foreach (var (key, value) in searchTerms
        //                .Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
        //            {
        //                PropertyInfo? prop = countryProps.Where(x => x.Name == key).FirstOrDefault();
        //                if (prop == null)
        //                {
        //                    throw new Exception("Property does not exist");
        //                }

        //                PropertyInfo? propSearch1 = countrySearchProps.Where(x => x.Name == key).FirstOrDefault();
        //                BaseValues.SearchType matchType = PredicateGenericHelper.GetSearchTypeForObject(propSearch1);

        //                //Check whether the value contains the | character - only works for equals
        //                if (value.Contains('|') && matchType == BaseValues.SearchType.Equals)
        //                {
        //                    condition = PredicateGenericHelper.CreateExpressionCallFromList<Country>(key, value, prop);
        //                }
        //                else
        //                {
        //                    PropertyInfo? propSearch = countrySearchProps.Where(x => x.Name == key).FirstOrDefault();
        //                    condition = PredicateGenericHelper.CreateExpressionCall<Country>(
        //                        key,
        //                        value,
        //                        PredicateGenericHelper.GetMethod(prop.PropertyType, matchType),
        //                        prop.PropertyType);
        //                }
        //                predicate = predicate.And(condition!);
        //                predicateApplied = true;
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(genericSearch))
        //        {
        //            predicate = predicate.And(x => x.Name.Contains(genericSearch));
        //            predicateApplied = true;
        //        }

        //        if (take == 0) { take = 20; }

        //        List<Country>? result = null;
        //        if (predicateApplied)
        //        {
        //            result = await _repository.Get(predicate, x => x.OrderByDescending(x => x.Id), take, skip);
        //        }
        //        else
        //        {
        //            result = await _repository.Get(null, null, take, skip);
        //        }

        //        if (result == null)
        //            result = new List<Country>();

        //        result.ForEach(x => countryModel.Add(MapCountry(x)));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in FindWarehouse");
        //    }
        //    return countryModel;
        //}

    }
}
