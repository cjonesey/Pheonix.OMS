using Phoenix.WebClient.Components.Base;
using System.Text;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class Warehouses : ListPageBase
    {
        protected List<WarehouseModel> _warehouses = new List<WarehouseModel>();
        [Inject] IWarehouseService? _warehouseService { get; set; }
        private string cdkOverlayPane;
        private WarehouseModel? _currentWarehouse;        
        private List<SearchModel> _searchModels = new();
        private string _searchTerm = string.Empty; 
 
        protected override async Task OnInitializedAsync()
        {
            var warehouses = await _warehouseService!.GetAllWarehouses();
            if (warehouses != null && warehouses.Any())
            {
                _warehouses = warehouses.ToList();
            }
        }


        protected void NewWarehouse()
        {
            _navigationManager.NavigateTo("/WarehouseEdit");
        }

        public async Task ExportToExcel()
        {
            if (_warehouses != null || _warehouses!.Any())
            {
                var excelBytes = ExcelService.GenerateExcelWorkbook<WarehouseModel>(_warehouses!);
                await _jsRuntime!.InvokeVoidAsync("saveAsFile", $"G2BPhoenix_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx", Convert.ToBase64String(excelBytes));
            }
        }

		protected override void ContextMenu(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("position: fixed;");
            sb.AppendLine($"top: {e.ClientY}px;");
            sb.AppendLine($"left: {e.ClientX}px;");
            sb.AppendLine("min-height: 10vh;");
            sb.AppendLine("width: auto;");
            sb.AppendLine("align-items: center;");
            sb.AppendLine("background-color: white;");
            cdkOverlayPane = sb.ToString();
            _contextMenuVisible = true;
		}

        private void SetCurrentRow(WarehouseModel warehouse)
        {
            _currentWarehouse = warehouse;
        }
        protected override void Delete()
        {
            if (_currentWarehouse == null)
                return;
            ShowDeleteDialogue();
        }

        private void Edit(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            if (_currentWarehouse == null)
                return;
            _navigationManager.NavigateTo($"/warehouseedit/{_currentWarehouse!.Id}");
		}


        /// <summary>
        /// Implements the actual delete operation from the base class
        /// </summary>
        /// <returns></returns>
        protected override async Task DeleteConfirmed()
        {
            showConfirmDialogue = false;
            if (_currentWarehouse == null)
                return;

            await _warehouseService!.DeleteWarehouse(_currentWarehouse!.Id);
            _warehouses.Remove(_currentWarehouse!);
            _currentWarehouse = null;
            toastService.ShowToast("Record Deleted", ToastLevel.Warning);
            Thread.Sleep(3000);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task ApplySearchFromWidget(List<SearchModel> searchModels)
        {
            if (searchModels == null || !searchModels.Any())
                return;
            _searchModels = searchModels;
            await ApplySearch();
        }

        protected void ClearSearchFromWidget()
        {
            _searchModels.Clear();
            _searchTerm = string.Empty;
            StateHasChanged();
        }

        protected async Task ApplySearchFromRibbon(string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return;
            _searchTerm = searchTerm;
            await ApplySearch();
        }

        private async Task ApplySearch()
        {
            Dictionary<string, string> searchTerms = new();
            foreach (var item in _searchModels)
            {
                searchTerms.Add(item.FieldName, item.Value);
            }
            var warehouses = await _warehouseService.FindWarehouse(searchTerms, _searchTerm);
            if (warehouses != null && warehouses.Any())
            {
                _warehouses = warehouses.ToList();
            }
        }
    }
}
