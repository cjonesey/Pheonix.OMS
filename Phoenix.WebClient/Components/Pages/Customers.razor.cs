using Microsoft.AspNetCore.Components.QuickGrid;
using Phoenix.Domain;
using Phoenix.WebClient.Components.Base;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class Customers : ListPageTemplate<CustomerModel>
    {
        [Inject] ICustomerService? _customerService { get; set; }
        object Data { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _model = new CustomerModel();
            int rowCount = 0;
            //gridProvider = async req =>
            //{
            //    var searchTerms = AddSearchTerms();
            //    var customers = await _customerService!.FindCustomer(searchTerms, _searchTerm, "", req.StartIndex, req.Count ?? 0);
            //    if (customers == null)
            //        customers = new List<CustomerModel>();

            //    if (customers!.Count() == req.Count)
            //    {
            //        rowCount = req.StartIndex + req.Count.Value + req.Count ?? 0;
            //    }
            //    else
            //    {
            //        rowCount = req.StartIndex + customers!.Count;
            //    }

            //    return GridItemsProviderResult.From(
            //        items: customers,
            //        totalItemCount: rowCount);
            //};
            Data = await _customerService!.GetAllCustomers();
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
            _navigationManager!.NavigateTo("/CustomerEdit");
        }

        public override Task LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
