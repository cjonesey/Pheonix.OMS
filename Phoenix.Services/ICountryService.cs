using Phoenix.Domain;
using Phoenix.Models.Shared;

namespace Phoenix.Services
{
    public interface ICountryService
    {
        Task<CountryModel?> AddCountry(CountryModel countryModel);
        Task<List<CountryModel>?> GetAllCountries();
        Task<CountryModel?> GetCountryById(int id);
        Task<CountryModel?> UpdateCountry(CountryModel countryModel);
    }
}