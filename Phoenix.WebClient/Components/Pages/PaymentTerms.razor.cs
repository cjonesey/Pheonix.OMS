﻿using Microsoft.AspNetCore.Components.QuickGrid;
using Phoenix.Domain;
using Phoenix.WebClient.Components.Base;

namespace Phoenix.WebClient.Components.Pages
{
	public partial class PaymentTerms : ListPageTemplate<PaymentTermModel>
    {
        [Inject] IGenericService<PaymentTerm, PaymentTermModel> ? _dataService { get; set; }
        List<PaymentTermModel> _paymentTerms; 

        protected override async Task OnInitializedAsync()
        {
            _model = new PaymentTermModel();
            _paymentTerms = new List<PaymentTermModel>();
            _paymentTerms = await _dataService!.GetAll(SimpleMapper.MapPaymentTermToPaymentTermModel);
            //int rowCount = 0;
            //gridProvider = async req =>
            //{
            //    var searchTerms = AddSearchTerms();
            //    var countries = await _dataService!.FindCountry(searchTerms, _searchTerm, "", req.StartIndex, req.Count ?? 0);
            //    if (countries == null)
            //        countries = new List<CountryModel>();

            //    if (countries!.Count() == req.Count)
            //    {
            //        rowCount = req.StartIndex + req.Count.Value + req.Count ?? 0;
            //    }
            //    else
            //    {
            //        rowCount = req.StartIndex + countries!.Count;
            //    }

            //    return GridItemsProviderResult.From(
            //        items: countries,
            //        totalItemCount: rowCount);
            //};
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

    }
}
