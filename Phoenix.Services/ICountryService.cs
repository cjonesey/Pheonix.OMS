using Phoenix.Domain;
using Phoenix.Models.Shared;

namespace Phoenix.Services
{
    public interface ICountryService
    {
        Task<CountryModel?> AddCountry(CountryModel countryModel);
        Task<List<CountryModel>> GetAllCountries();
        Task<List<CountryModel>?> FindCountry(
            Dictionary<string, string> searchTerms,
            string genericSearch,
            string sortBy,
            int skip,
            int take);
        Task<CountryModel?> GetCountryById(int id);
        Task<CountryModel?> UpdateCountry(CountryModel countryModel);
    }
}