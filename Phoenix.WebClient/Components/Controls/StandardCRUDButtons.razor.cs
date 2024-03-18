using Microsoft.JSInterop;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class StandardCRUDButtons
    {
        [Inject] IJSRuntime js { get; set; }
        [Parameter] public EventCallback SaveInvoked { get; set; }
        [Parameter] public EventCallback CancelInvoked { get; set; }
        [Parameter] public EventCallback NewInvoked { get; set; }

        public async void HandleSave()
        {
            await SaveInvoked.InvokeAsync();
        }

        public async void HandleCancel ()
        {
            await CancelInvoked.InvokeAsync();
        }

        public async void HandleNew()
        {
            await NewInvoked.InvokeAsync();
        }
    }
}
