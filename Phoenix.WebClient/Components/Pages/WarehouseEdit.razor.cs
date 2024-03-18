namespace Phoenix.WebClient.Components.Pages
{
    public partial class WarehouseEdit
    {
        [Inject] IJSRuntime js { get; set; }
        [Inject] public NavigationManager? NavigationManager { get; set; }
        [Parameter] public string? Id { get; set; }
        [SupplyParameterFromForm] protected WarehouseModel _warehouse { get; set; }
        [Inject] protected IWarehouseService _warehouseService { get; set; }
        
        public string searchString = "";

        protected EditContext? editContext;

        private ValidationMessageStore? messageStore;

        protected bool dataIsLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            if (_warehouse == null)
            {
                _warehouse = new WarehouseModel();

                if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out int idSearch))
                {
                    var warehouse = await _warehouseService.GetWarehouseById(idSearch);
                    if (warehouse != null)
                    {
                        _warehouse = new WarehouseModel
                        {
                            Id = warehouse.Id,
                            Name = warehouse.Name,
                            Street1 = warehouse.Street1,
                            Street2 = warehouse.Street2,
                            City = warehouse.City,
                            County = warehouse.County,
                            Postcode = warehouse.Postcode,
                            CreatedOn = warehouse.CreatedOn,
                            ModifiedOn = warehouse.ModifiedOn,
                            ChangeCheck = warehouse.ChangeCheck,
                            CountryId = warehouse.CountryId,
                            CountryCode = warehouse.CountryCode
                        };
                    }
                }
            }
            editContext = new(_warehouse);
            messageStore = new(editContext);
            editContext!.OnValidationRequested += HandleValidationRequested;
            dataIsLoaded = true;
        }

        bool countryLookup = false;

        async Task HandleInput(ChangeEventArgs e)
        {
            countryLookup = true;
            searchString = e.Value?.ToString();
        }

        private void HandleValidationRequested(object? sender,
            ValidationRequestedEventArgs args)
        {
            messageStore?.Clear();
            ValidationContext context = new ValidationContext(_warehouse, null, null);
            ICollection<ValidationResult>? validationResults = new List<ValidationResult>();
            if(!Validator.TryValidateObject(_warehouse, context, validationResults, true))
            {
                foreach (var result in validationResults)
                {
                    messageStore?.Add(() => result.MemberNames, result.ErrorMessage!);
                }
            }
        }

        void SelectCountry(CountryModel? country)
        {
            if (country == null) return;
            _warehouse.CountryId = country.Id;
            _warehouse.CountryCode = country.Code;
            countryLookup = false;
        }

        protected async Task ValidateAndSave()
        {
            var isValid = editContext!.Validate();
            if (isValid)
            {
                await OnValidSubmit();
            }
        }

        private async Task OnValidSubmit()
        {
            WarehouseModel? warehouse = default;
            if (_warehouse.Id == 0)
            {
                warehouse = await _warehouseService!.AddWarehouse(_warehouse);
            }
            else
            {
                warehouse = await _warehouseService.UpdateWarehouse(_warehouse);
            }
            if (warehouse != null)
            {
                _warehouse = warehouse;
            }
        }

        private void OnCancel()
        {
            NavigationManager!.NavigateTo("/warehouses");
        }
        private void OnNew()
        {
            _warehouse = new WarehouseModel();
        }


        private async void NavigateBack()
        {
            await js.InvokeVoidAsync("history.back");
        }
    }
}
