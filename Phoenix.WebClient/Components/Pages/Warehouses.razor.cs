using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Phoenix.Domain;
using Phoenix.WebClient.Services;
using System.Text;

namespace Phoenix.WebClient.Components.Pages
{
    public partial class Warehouses
    {
        protected List<WarehouseModel> _warehouses = new List<WarehouseModel>();
        protected string selectBox = "visually-hidden";
        [Inject] IWarehouseService _warehouseService { get; set; }
        [Inject] IJSRuntime _jsRuntime { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] public ToastService toastService { get; set; }

        private bool _contextMenuVisible = false;
        private string cdkOverlayPane;
        private bool _contextTriggered = false;
        private WarehouseModel? _currentWarehouse;
        protected bool showConfirmDialogue = false;

        public Warehouses()
        {
        }
        protected override async Task OnInitializedAsync()
        {
            var warehouses = await _warehouseService.GetAllWarehouses();
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

		private void ContextMenu(global::Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
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
		private void HideContext(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            if (!_contextMenuVisible && !_contextTriggered)
            {
                return;
            }
            if (_contextMenuVisible && !_contextTriggered)
            {
                _contextTriggered = true;
                return;
            }
            _contextTriggered = false;
            _contextMenuVisible = false;   

        }
        private void SetCurrentRow(WarehouseModel warehouse)
        {
            _currentWarehouse = warehouse;
        }
		private void Delete(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            if (_currentWarehouse == null)
                return;
            showConfirmDialogue = true;
        }
		private void Edit(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
		{
            if (_currentWarehouse == null)
                return;
            _navigationManager.NavigateTo($"/warehousedit/{_currentWarehouse!.Id}");
		}
        private void CancelDelete()
        {
            showConfirmDialogue = false;
        }

        protected async Task HandleDelete()
        {
            showConfirmDialogue = false;
            if (_currentWarehouse == null)
                return;

            await _warehouseService.DeleteWarehouse(_currentWarehouse!.Id);
            _warehouses.Remove(_currentWarehouse!);
            _currentWarehouse = null;
            toastService.ShowToast("Record Deleted", ToastLevel.Warning);
            Thread.Sleep(3000);
            await InvokeAsync(StateHasChanged);
        }
    }
}
