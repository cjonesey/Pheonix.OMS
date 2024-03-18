using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Phoenix.Models.Shared;
using Phoenix.Services;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class CountryEdit
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Parameter] public string? id { get; set; }
        [SupplyParameterFromForm] public CountryModel Model { get; set; }
        [Inject] protected ILogger<CountryEdit>? _logger { get; set; }
        [Inject] protected ICountryService _countryService { get; set; }
        [Parameter] public bool recordLoaded { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            //We already have a model, so don't refresh the record
            if (Model != null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out var countryId))
            {
                var country = await _countryService.GetCountryById(countryId);
                if (country != null)
                {
                     Model = country;
                }
            }
            else
            {
                Model = new CountryModel();
            }
            recordLoaded = true;
        }

        private void OnValidSubmit()
        {
            _countryService.UpdateCountry(Model);
        }
        private void Submit()
        {
            _logger.LogInformation("Id = {Id}", Model.Id);
        }
        private void OnInvalidSubmit()
        {
            _logger.LogError("Invalid form submission");
        }

        private void OnCancel()
        {
            NavigationManager.NavigateTo("/countries");
        }
    }
}
