using Microsoft.AspNetCore.Components.Forms;
using Phoenix.WebClient.Services;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class CountryEditComponent
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Parameter] public int? id { get; set; }
        [SupplyParameterFromForm] public CountryModel? _model { get; set; }
        [Inject] protected ILogger<CountryEditComponent>? _logger { get; set; }
        [Inject] protected ICountryService? _countryService { get; set; }
        [Parameter] public bool dataIsLoaded { get; set; } = false;
        [Parameter] public EventCallback CloseInvoked { get; set; }
        [Inject] public ToastService? toastService { get; set; }

        private ValidationMessageStore? _messageStore;
        private bool _showConfirmDialogue = false;
        private EditContext? _editContext;
        protected override async Task OnInitializedAsync()
        {
            if (_countryService == null)
                return;

            //We already have a model, so don't refresh the record
            if (_model != null)
                return;

            if (id.HasValue)
            {
                var country = await _countryService.GetCountryById(id.Value);
                if (country != null)
                {
                    _model = country;
                }
            }
            else
            {
                _model = new CountryModel();
            }

            _editContext = new(_model!);
            _messageStore = new(_editContext);
            _editContext!.OnValidationRequested += HandleValidationRequested;
            dataIsLoaded = true;
        }
        private void HandleValidationRequested(object? sender,
            ValidationRequestedEventArgs args)
        {
            if (_model == null)
                return;
            _messageStore?.Clear();
            ValidationContext context = new ValidationContext(_model, null, null);
            ICollection<ValidationResult>? validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(_model, context, validationResults, true))
            {
                foreach (var result in validationResults)
                {
                    _messageStore?.Add(() => result.MemberNames, result.ErrorMessage!);
                }
            }
        }
        private void Submit()
        {
            if (_model == null)
                return;
            _logger!.LogInformation("Id = {Id}", _model!.Id);
        }
        private void OnInvalidSubmit()
        {
            _logger!.LogError("Invalid form submission");
        }

        private async Task ValidateAndSave()
        {
            var isValid = _editContext!.Validate();
            if (isValid)
            {
                await OnValidSubmit();
            }
        }
        private async Task OnValidSubmit()
        {
            if (_model == null)
                return;

            CountryModel? model = default;
            try
            {
                if (_model.Id == 0)
                {
                    model = await _countryService!.AddCountry(_model);
                }
                else
                {
                    model = await _countryService!.UpdateCountry(_model);
                }

                if (model != null)
                {
                    toastService!.ShowToast("Record Saved", ToastLevel.Success);
                    Thread.Sleep(3000);
                    await CloseInvoked.InvokeAsync();
                    return;
                }
                toastService!.ShowToast("Record Not saved", ToastLevel.Error);
                return;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex?.ToString());
                toastService!.ShowToast("Record Not saved", ToastLevel.Error);
            }
        }

        protected async Task Back(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await CloseInvoked.InvokeAsync();
        }
        protected async Task OnCancel()
        {
            await CloseInvoked.InvokeAsync();
        }
        protected async Task OnNew()
        {
            _model = new CountryModel();
            await InvokeAsync(StateHasChanged);
        }
        private void OpenConfirmDialog()
        {
            _showConfirmDialogue = true;
        }

    }
}
