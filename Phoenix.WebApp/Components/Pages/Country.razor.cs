using Microsoft.AspNetCore.Components;
using Phoenix.Infrastructure;
using Phoenix.Services;
using Entity = Phoenix.Domain;

namespace Phoenix.WebApp.Components.Pages
{
    public partial class Country
    {
        private List<Entity.Country> _countries = new List<Entity.Country>();

        [Inject] public ILogger<Country>? _logger { get; set; }
        [Inject] public ICountryService _countryService { get; set; }



        public Country()
        {
        }
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
