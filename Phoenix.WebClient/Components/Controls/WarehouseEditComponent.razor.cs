namespace Phoenix.WebClient.Components.Controls
{
    public partial class WarehouseEditComponent
    {
        [Parameter] public int? id { get; set; }
        [SupplyParameterFromForm] protected WarehouseModel? _warehouse { get; set; } 
        [Inject] protected IWarehouseService? _warehouseService { get; set; }
        [Inject] public ToastService? toastService { get; set; }
        [Inject] public ILogger<WarehouseEditComponent> _logger { get; set; }        
        [Parameter] public EventCallback CloseInvoked { get; set; } //Should add in a delete method to update the grid??

        public string searchString = "";

        protected EditContext? editContext;

        private ValidationMessageStore? messageStore;

        protected bool dataIsLoaded = false;
        protected bool showConfirmDialogue = false;

        protected override async Task OnInitializedAsync()
        {
            if (_warehouse == null)
            {
                _warehouse = new WarehouseModel();

                if (id.HasValue && id.Value != 0)
                {
                    var warehouse = await _warehouseService!.GetWarehouseById(id.Value);
                    if (warehouse != null)
                    {
                        MapWarehouse(warehouse);
                    }
                }
            }
            editContext = new(_warehouse);
            messageStore = new(editContext);
            editContext!.OnValidationRequested += HandleValidationRequested;
            dataIsLoaded = true;
        }

        private void MapWarehouse(WarehouseModel warehouse)
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

        bool countryLookup = false;

        async Task HandleInput(ChangeEventArgs e)
        {
            countryLookup = true;
            searchString = e.Value?.ToString();
        }

        private void HandleValidationRequested(object? sender,
            ValidationRequestedEventArgs args)
        {
            if (_warehouse == null)
                return;
            messageStore?.Clear();
            ValidationContext context = new ValidationContext(_warehouse, null, null);
            ICollection<ValidationResult>? validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(_warehouse, context, validationResults, true))
            {
                foreach (var result in validationResults)
                {
                    messageStore?.Add(() => result.MemberNames, result.ErrorMessage!);
                }
            }
        }

        void SelectCountry(CountryModel? country)
        {
            if (_warehouse == null || country == null)
                return;
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
            if (_warehouse == null)
                return; 

            WarehouseModel? warehouse = default;
            try
            {
                if (_warehouse.Id == 0)
                {
                    warehouse = await _warehouseService!.AddWarehouse(_warehouse);
                }
                else
                {
                    warehouse = await _warehouseService!.UpdateWarehouse(_warehouse);
                }

                if (warehouse != null)
                {
                    MapWarehouse(warehouse);
                    toastService!.ShowToast("Record Saved", ToastLevel.Success);
                    Thread.Sleep(3000);
                    //NavigationManager!.NavigateTo("/Warehouses");
                    await CloseInvoked.InvokeAsync();
                    return;
                }
                toastService!.ShowToast("Record Not saved", ToastLevel.Error);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.ToString());
                toastService!.ShowToast("Record Not saved", ToastLevel.Error);
            }
        }

        private async Task OnCancel()
        {
            await CloseInvoked.InvokeAsync();
        }
        private void OnNew()
        {
            _warehouse = new WarehouseModel();
        }

        private void OpenConfirmDialog()
        {
            showConfirmDialogue = true;
        }

        private void CancelDelete()
        {
            showConfirmDialogue = false;
        }

        protected async Task HandleDelete()
        {
            if (_warehouse == null)
                return;
            showConfirmDialogue = false;
            await InvokeAsync(StateHasChanged);
            await _warehouseService!.DeleteWarehouse(_warehouse.Id);
            toastService!.ShowToast("Record Deleted", ToastLevel.Warning);
            Thread.Sleep(3000);
            await CloseInvoked.InvokeAsync();
            //NavigationManager!.NavigateTo("/Warehouses");
        }
        private async Task Back(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await CloseInvoked.InvokeAsync();
        }

    }
}
