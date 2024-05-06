

using Phoenix.Shared;

namespace Phoenix.Services
{
	public class CountryService : ICountryService
    {
        public ILogger<CountryService> _logger;
        private readonly IRepositoryBase<Country> _repository;

        public CountryService(ILogger<CountryService> logger,
            IRepositoryBase<Country> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<List<CountryModel>> GetAllCountries()
        {
            List<CountryModel> countryModels = new();
            var countries = await _repository.GetAll();
            if (countries != null && countries.Any())
            {
                countries.ToList().ForEach(x => countryModels.Add(MapCountry(x)));
            }
            return countryModels;
        }

    public async Task<CountryModel?> GetCountryById(int id)
        {
            var country = await _repository.Get(id);
            if (country != null)
            {
                return MapCountry(country);
            }
            return default;
        }

        private static CountryModel MapCountry(Country country)
        {
            return new CountryModel
            {
                Id = country.Id,
                Code = country.Code,
                ISOCode2 = country.ISOCode2,
                Name = country.Name,
                CreatedOn = country.CreatedOn,
                ModifiedOn = country.ModifiedOn,
                ChangeCheck = country.ChangeCheck
            };
        }

        public async Task<CountryModel?> AddCountry(CountryModel countryModel)
        {
            var country = new Country
            {
                Code = countryModel.Code,
                ISOCode2 = countryModel.ISOCode2,
                Name = countryModel.Name,
                ModifiedOn = DateTime.Now,
                CreatedOn = DateTime.Now
            };
            await _repository.Add(country, country.Id);
            return await GetCountryById(country.Id);
        }

        public async Task<CountryModel?> UpdateCountry(CountryModel countryModel)
        {
            var country = new Country
            {
                Id = countryModel.Id,
                Code = countryModel.Code,
                ISOCode2 = countryModel.ISOCode2,
                Name = countryModel.Name,
                CreatedOn = countryModel.CreatedOn,
                ModifiedOn = DateTime.Now,
                ChangeCheck = countryModel.ChangeCheck
            };
            await _repository.Update(country, country.Id);
            return await GetCountryById(country.Id);
        }

        public async Task<List<CountryModel>?> FindCountry(Dictionary<string, string> searchTerms, string genericSearch, string sortBy, int skip, int take)
        {
            var predicate = PredicateBuilder.True<Country>();
            var countryProps = typeof(Country).GetProperties();
            var countrySearchProps = typeof(CountryModel).GetProperties();
            List<Country>? countries = new();
            List<CountryModel> countryModel = new();
            List<PropertyInfo> differences = new List<PropertyInfo>();
            Expression<Func<Country, bool>> condition = default;
            bool predicateApplied = false;

            try
            {
                if (searchTerms != null && searchTerms.Any())
                {
                    foreach (var (key, value) in searchTerms
                        .Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
                    {
                        PropertyInfo? prop = countryProps.Where(x => x.Name == key).FirstOrDefault();
                        if (prop == null)
                        {
                            throw new Exception("Property does not exist");
                        }

                        PropertyInfo? propSearch1 = countrySearchProps.Where(x => x.Name == key).FirstOrDefault();
                        BaseValues.SearchType matchType = PredicateGenericHelper.GetSearchTypeForObject(propSearch1);

                        //Check whether the value contains the | character - only works for equals
                        if (value.Contains('|') && matchType == BaseValues.SearchType.Equals)
                        {
                            condition = PredicateGenericHelper.CreateExpressionCallFromList<Country>(key, value, prop.PropertyType);
                        }
                        else
                        {
                            PropertyInfo? propSearch = countrySearchProps.Where(x => x.Name == key).FirstOrDefault();
                            condition = PredicateGenericHelper.CreateExpressionCall<Country>(
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

                List<Country>? result = null;
                if (predicateApplied)
                {
                    result = await _repository.Get(predicate, x => x.OrderByDescending(x => x.Id), take, skip);
                }
                else
                {
                    result = await _repository.Get(null, null, take, skip);
                }

                if (result == null)
                    result = new List<Country>();

                result.ForEach(x => countryModel.Add(MapCountry(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FindWarehouse");
            }
            return countryModel;
        }
    }
}
