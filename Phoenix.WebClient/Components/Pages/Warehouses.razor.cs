namespace Phoenix.WebClient.Components.Pages
{
    public partial class Warehouses
    {
        protected List<WarehouseModel> _warehouses = new List<WarehouseModel>();

        protected string selectBox = "visually-hidden";
        [Inject] IWarehouseService _warehouseService { get; set; }
        [Inject] IJSRuntime _jsRuntime { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }


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
    }
}
