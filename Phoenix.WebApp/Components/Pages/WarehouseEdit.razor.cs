using Microsoft.AspNetCore.Components;
using Pheonix.OMS.Domain;
using Pheonix.OMS.Domain.Migrations;
using Pheonix.OMS.Domain.Repository;
using Phoenix.WebApp.Components.Parts;
using static System.Net.WebRequestMethods;

using Entity = Pheonix.OMS.Domain;

namespace Phoenix.WebApp.Components.Pages
{
    public partial class WarehouseEdit
    {
        private Modal modal { get; set; }
        [Parameter]
        public string? Id { get; set; }
        [Parameter]
        public Entity.Warehouse warehouse { get; set; } = new();
        public Dictionary<int, string> _countries { get; set; } = new();

        [Inject]
        public IRepositoryBase<Entity.Warehouse> _warehouseRepo { get; set; }
        [Inject] 
        public IRepositoryBase<Entity.Country> _countryRepo { get; set; }


        protected override async Task OnInitializedAsync()
        {
            int idSearch = 0;
            if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out idSearch))
            {
                var warehouse = await _warehouseRepo.Get(idSearch);
                if (warehouse != null)
                {
                    this.warehouse = warehouse;
                }
                if (!_countries.Any())
                {
                    var countries = await _countryRepo.GetAll();
                    if (countries != null && countries.Any())
                    {
                        _countries = countries.Select(x=> new KeyValuePair<int, string>(x.Id, x.Name)).ToDictionary(x=>x.Key, x=>x.Value);
                    }
                }
            }
        }

        //Autocomplete
        List<Country>? countries;
        List<Country>? allCountries;
        int? selectedCountryId;
        string? selectedCountryName;
        string? filter;

        async Task HandleInput(ChangeEventArgs e)
        {
           if (allCountries == null || !allCountries.Any())
            {
                var result = await _countryRepo.GetAll();
                if (result != null && result.Any())
                {
                    allCountries = result.ToList();
                }
            } 

            filter = e.Value?.ToString();
            if (allCountries == null || !allCountries.Any()) return;

            if (filter?.Length < 2)
            {
                countries = allCountries.Take(10).ToList();
                selectedCountryName = string.Empty;
                selectedCountryId = null;
                return;
            }
            countries = allCountries.Where(c => 
                c.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase)
                || c.Code.StartsWith(filter!, StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
        }

        void SelectCountry(int id)
        {
            selectedCountryId = id;
            selectedCountryName = countries!.First(c => c.Id == id).Code;
            warehouse.CountryCode = selectedCountryName;
            countries = null;
        }
        private void OnValidSubmit()
        {
            // Perform the desired action on form submission
        }
    }
}
