using Phoenix.WebClient.Components.Pages;

namespace Phoenix.WebClient.Components.Parts
{
    public partial class CountryLookup
    {
        private List<CountryModel>? countries;
        private List<CountryModel> allCountries = new List<CountryModel>();
        private int? selectedCountryId;
        private string? selectedCountryName;
        private string? filter;

        [Inject] public ICountryService? _countryService { get; set; }

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

        [Parameter] public EventCallback<CountryModel> OnCountrySelected { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //if (!allCountries.Any())
            //{
            //    await GetAllCountries();
            //}
            //if (!string.IsNullOrEmpty(_lookupText))
            //{ 
            //    countries = allCountries.Where(c =>
            //        c.Name.Contains(_lookupText!, StringComparison.OrdinalIgnoreCase)
            //        || c.Code.StartsWith(_lookupText!, StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            //}
        }

        protected async Task HandleInput()
        {
            if (!allCountries.Any())
            {
                await GetAllCountries();
            }

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
        private async Task GetAllCountries()
        {
            var result = await _countryService!.GetAllCountries();
            if (result != null && result.Any())
            {
                allCountries = result.ToList();
                countries = allCountries.Take(10).ToList();
            }
        }
    }

}
