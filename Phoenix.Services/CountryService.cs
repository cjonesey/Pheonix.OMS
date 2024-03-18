using Microsoft.Extensions.Logging;
using Phoenix.Domain;
using Phoenix.Infrastructure;
using Phoenix.Models.Shared;

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
        public async Task<List<CountryModel>?> GetAllCountries()
        {
            var countries = await _repository.GetAll();
            if (countries != null && countries.Any())
            {
                return countries.Select(c => new CountryModel
                {
                    Id = c.Id,
                    Code = c.Code,
                    ISOCode2 = c.ISOCode2,
                    Name = c.Name
                }).ToList();
            }
            return default;
        }

        public async Task<CountryModel?> GetCountryById(int id)
        {
            var country = await _repository.Get(id);
            if (country != null)
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
            return default;
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
    }
}
