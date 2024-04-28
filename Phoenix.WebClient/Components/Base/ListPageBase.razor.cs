using Microsoft.AspNetCore.Components.QuickGrid;

namespace Phoenix.WebClient.Components.Base
{
    public abstract partial class ListPageBase
    {
        [Inject] protected IJSRuntime? _jsRuntime { get; set; }
        [Inject] protected NavigationManager? _navigationManager { get; set; }
        [Inject] protected ToastService? toastService { get; set; }

        protected bool showConfirmDialogue = false;
        protected bool _contextTriggered = false;
        protected bool _contextMenuVisible = false;
        protected void ShowDeleteDialogue() => showConfirmDialogue = true;
        protected void CancelDeleteDialogue() => showConfirmDialogue = false;
        protected abstract Task DeleteConfirmed();
        protected abstract void HandleDelete();
        protected abstract void ContextMenu(Microsoft.AspNetCore.Components.Web.MouseEventArgs e);
        protected void HideContext(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
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

    }
}
