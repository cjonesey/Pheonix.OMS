using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components.QuickGrid;
using Phoenix.WebClient.Components.Base;
using System;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class Warehouses : ListPageBase
    {
        //Define the model for the searching
        private WarehouseModel _warehouse = new WarehouseModel();
        
        //Warehouse that is selected
        private WarehouseModel? _currentWarehouse;

        //Service to get the data -- this could be an API
        [Inject] IWarehouseService? _warehouseService { get; set; }

        //Number of records to display - if required
        int numResults;

        //Grid Operation
        GridItemsProvider<WarehouseModel>? warehousesProvider;
        QuickGrid<WarehouseModel>? warehouseGrid;

        //Search Widgets
        private List<SearchModel> _searchModels = new();
        private string _searchTerm = string.Empty;

        private string cdkOverlayPane;
        private string sortBy = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            int rowCount = 0;
            warehousesProvider = async req =>
            {
                var searchTerms = AddSearchTerms();
                var warehouses = await _warehouseService!.FindWarehouse(searchTerms, _searchTerm, "", req.StartIndex, req.Count ?? 0);
                if (warehouses!.Count() == req.Count)
                {
                    rowCount = req.StartIndex + req.Count.Value + req.Count ?? 0;
                }
                else
                {
                    rowCount = req.StartIndex + warehouses!.Count;
                }

                return GridItemsProviderResult.From(
                    items: warehouses,
                    totalItemCount: rowCount);
            };
        }

        protected async Task SortBy(string columnName = "") 
        { 
            sortBy = columnName;
            //await ApplySearch();
        }
        protected void NewWarehouse()
        {
            _navigationManager.NavigateTo("/WarehouseEdit");
        }

        public async Task ExportToExcel()
        {
            //if (_warehouses != null || _warehouses!.Any())
            //{
            //    var excelBytes = ExcelService.GenerateExcelWorkbook<WarehouseModel>(_warehouses!);
            //    await _jsRuntime!.InvokeVoidAsync("saveAsFile", $"G2BPhoenix_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx", Convert.ToBase64String(excelBytes));
            //}
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
            //_warehouses.Remove(_currentWarehouse!);
            _currentWarehouse = null;
            toastService.ShowToast("Record Deleted", ToastLevel.Warning);
            Thread.Sleep(3000);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task ApplySearchFromWidget(List<SearchModel> searchModels)
        {
            if (searchModels == null || !searchModels.Any() || warehouseGrid == null)
                return;
            _searchModels = searchModels;
            await warehouseGrid.RefreshDataAsync();
        }

        protected async Task ClearSearchFromWidget()
        {
            _searchModels.Clear();
            _searchTerm = string.Empty;
            if (warehouseGrid != null)
            {
                await warehouseGrid.RefreshDataAsync();
                StateHasChanged();
            }
        }

        protected async Task ApplySearchFromRibbon(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || warehouseGrid == null)
                return;
            _searchTerm = searchTerm;
            await warehouseGrid.RefreshDataAsync();
        }

        private Dictionary<string, string> AddSearchTerms()
        {
            Dictionary<string, string> searchTerms = new();
            _searchModels.Where(x => !string.IsNullOrEmpty(x.FieldName) && !string.IsNullOrEmpty(x.Value))
                .ToList()
                .ForEach(x => searchTerms.Add(x.FieldName, x.Value));
            return searchTerms;
        }
    }
}
