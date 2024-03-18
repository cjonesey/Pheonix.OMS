using Microsoft.Identity.Client;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class StandardListButtons
    {
        protected string _searchText { get; set; } = string.Empty;
        [Parameter] public EventCallback SearchInvoked { get; set; }
        [Parameter] public EventCallback DeleteInvoked { get; set; }
        [Parameter] public EventCallback NewInvoked { get; set; }

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
    }
}
