using Microsoft.Identity.Client;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class StandardListButtons
    {
        [Parameter] public string MenuName { get; set; } = "List Page";
        protected string _searchText { get; set; } = string.Empty;
        [Parameter] public EventCallback SearchInvoked { get; set; }
        [Parameter] public EventCallback DeleteInvoked { get; set; }
        [Parameter] public EventCallback NewInvoked { get; set; }
        [Parameter] public EventCallback ExportInvoked { get; set; }

        public async void HandleSearch()
        {
            await SearchInvoked.InvokeAsync();
        }

        public async void HandleCancel()
        {
            await DeleteInvoked.InvokeAsync();
        }

        public async void HandleNew()
        {
            await NewInvoked.InvokeAsync();
        }

        public async void HandleExport()
        {
            await ExportInvoked.InvokeAsync();
        }
    }
}
