using Microsoft.AspNetCore.Components;
using Pheonix.OMS.Domain;
using Pheonix.OMS.Domain.Repository;
using Phoenix.WebApp.Components.Pages;
using Entity = Pheonix.OMS.Domain;

namespace Phoenix.WebApp.Components.Parts
{
    public partial class CountryLookup
    {
        [Inject]
        public IRepositoryBase<Entity.Country> _countryRepo { get; set; }

        [Parameter]
        public string LookupText
        {
            get { return _lookupText; }
            set
            {
                _lookupText = value;
                AsyncRunner.Run(HandleInput());
            }
        }

        private string _lookupText = "";

        [Parameter]
        public EventCallback<Country> OnCountrySelected { get; set; }


        List<Country>? countries;
        List<Country>? allCountries;
        int? selectedCountryId;
        string? selectedCountryName;
        string? filter;

        protected override async Task OnInitializedAsync()
        {
            if (allCountries == null || !allCountries.Any())
            {
                var result = await _countryRepo.GetAll();
                if (result != null && result.Any())
                {
                    allCountries = result.ToList();
                    countries = allCountries.Take(10).ToList();
                }
            }
            if (!string.IsNullOrEmpty(_lookupText))
            { 
                countries = allCountries.Where(c =>
                                   c.Name.Contains(_lookupText!, StringComparison.OrdinalIgnoreCase)
                                                      || c.Code.StartsWith(_lookupText!, StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            }
        }


        async Task HandleInput()
        {
            if (allCountries == null || !allCountries.Any())
            {
                var result = await _countryRepo.GetAll();
                if (result != null && result.Any())
                {
                    allCountries = result.ToList();
                }
            }

            //filter = e.Value?.ToString();
            filter = _lookupText;
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

        public async Task SelectCountry(int id)
        {
            selectedCountryId = id;
            var country = countries!.First(c => c.Id == id);
            await OnCountrySelected.InvokeAsync(country);
            countries = null;
        }
    }

    public class AsyncRunner
    {
        public static void Run(Task task, Action<Task> onError = null)
        {
            if (onError == null)
            {
                task.ContinueWith((task1, o) => { }, TaskContinuationOptions.OnlyOnFaulted);
            }
            else
            {
                task.ContinueWith(onError, TaskContinuationOptions.OnlyOnFaulted);
            }
        }
    }
}
