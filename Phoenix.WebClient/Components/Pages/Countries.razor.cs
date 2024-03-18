using Microsoft.AspNetCore.Components;
using Phoenix.Domain;
using Phoenix.Models.Shared;
using Phoenix.Services;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class Countries
    {
        private List<CountryModel> _countries = new List<CountryModel>();
        [Inject] public ILogger<Countries>? _logger { get; set; }
        [Inject] public ICountryService _countryService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var coutries = await _countryService.GetAllCountries();
            if (coutries != null && coutries.Any())
            {
                _countries = coutries.ToList();
            }
        }
    }
}
