using Microsoft.Identity.Client;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class StandardListButtons
    {
        [Parameter] public string MenuName { get; set; } = "List Page";
        protected string _searchText { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> SearchInvoked { get; set; }
        [Parameter] public EventCallback DeleteInvoked { get; set; }
        [Parameter] public EventCallback NewInvoked { get; set; }
        [Parameter] public EventCallback ExportInvoked { get; set; }
        [Parameter] public EventCallback RefreshInvoked { get; set; }

        public async void HandleSearch()
        {
            await SearchInvoked.InvokeAsync(_searchText);
        }

        public async void HandleCancel(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await DeleteInvoked.InvokeAsync();
        }

        public async void HandleNew(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await NewInvoked.InvokeAsync();
        }

        public async void HandleExport(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await ExportInvoked.InvokeAsync();
        }
        private async void HandleDelete(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await DeleteInvoked.InvokeAsync();
        }
        private async void HandleRefresh(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await RefreshInvoked.InvokeAsync();
        }
    }
}
