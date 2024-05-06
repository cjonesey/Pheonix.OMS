using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.Extensions.Logging;
using Phoenix.Domain;
using Phoenix.WebClient.Components.Base;

namespace Phoenix.WebClient.Components.Pages
{
	public partial class PaymentTerms : ListPageTemplate<PaymentTermModel>
    {
        [Inject] IGenericService<PaymentTerm, PaymentTermModel> ? _dataService { get; set; }

        protected override async Task OnInitializedAsync()
		{
            _ModelValues = new List<PaymentTermModel>();
			_model = new PaymentTermModel();
		}

		protected override async void OnAfterRender(bool firstRender)
		{
            if (firstRender == true)
            {
				await LoadData();
			}
		}

		public override async Task LoadData()
		{
            if (_navigationRequired == true && _recordsLoaded <= _maxRecords)
            {
                _navigationRequired = false;
                var paymentTermsLoad = await _dataService!.FindRecords(AddSearchTerms(), _searchTerm, _sortModel, SimpleMapper.MapPaymentTermToPaymentTermModel, _recordsLoaded, _pageSize);
		        if (paymentTermsLoad != null && paymentTermsLoad.Any())
                {
                    paymentTermsLoad.ForEach(x => _ModelValues.Add(x));
                    _recordsLoaded = _ModelValues.Count();
                    if (paymentTermsLoad.Count >= _pageSize)
                    {
                        _navigationRequired = true;
                    }
                    await InvokeAsync(StateHasChanged);
				}
			}
		}

		protected override async Task DeleteConfirmed()
        {
            throw new NotImplementedException();
            //showConfirmDialogue = false;
            //if (_currentModel == null)
            //    return;
            ////await _countryService!.DeleteWarehouse(_currentWarehouse!.Id);
            //_currentModel = null;
            //toastService.ShowToast("Record Deleted", ToastLevel.Warning);
            //Thread.Sleep(3000);
            //await InvokeAsync(StateHasChanged);
        }

        protected void HandleNew()
        {
            _navigationManager!.NavigateTo("/CountryEdit");
        }

		private async void LoadMore(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            await LoadData();
			await InvokeAsync(StateHasChanged);
		}

	}
}
