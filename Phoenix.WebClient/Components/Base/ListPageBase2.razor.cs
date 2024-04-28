//using Microsoft.AspNetCore.Components.QuickGrid;
//using Phoenix.WebClient.Components.Base;
//using System.Text;

//namespace Phoenix.WebClient.Components.Base
//{
//    public abstract partial class ListPageBase2<T>
//    {
//        [Inject] protected IJSRuntime? _jsRuntime { get; set; }
//        [Inject] protected NavigationManager? _navigationManager { get; set; }
//        [Inject] protected ToastService? toastService { get; set; }

//        protected bool showConfirmDialogue = false;
//        protected bool _contextTriggered = false;
//        protected bool _contextMenuVisible = false;
//        protected void ShowDeleteDialogue() => showConfirmDialogue = true;
//        protected void CancelDeleteDialogue() => showConfirmDialogue = false;
//        protected abstract Task DeleteConfirmed();

//        //Define the model for the searching
//        protected T? _model;
//        protected T? _currentModel;
//        //Number of records to display - if required
//        protected int numResults;

//        //Grid Operation
//        protected GridItemsProvider<T>? gridProvider;
//        protected QuickGrid<T>? gridView;

//        //Search Widgets
//        protected List<SearchModel> _searchModels = new();
//        protected string _searchTerm = string.Empty;
//        protected string cdkOverlayPane;
//        protected string sortBy = string.Empty;

//        protected void SetCurrentRow(T model)
//        {
//            _currentModel = model;
//        }
//        protected bool _editMode = false;

//        protected void HideContext(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
//        {
//            if (!_contextMenuVisible && !_contextTriggered)
//            {
//                return;
//            }
//            if (_contextMenuVisible && !_contextTriggered)
//            {
//                _contextTriggered = true;
//                return;
//            }
//            _contextTriggered = false;
//            _contextMenuVisible = false;
//        }

//        protected void ContextMenu(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine("position: fixed;");
//            sb.AppendLine($"top: {e.ClientY}px;");
//            sb.AppendLine($"left: {e.ClientX}px;");
//            sb.AppendLine("min-height: 10vh;");
//            sb.AppendLine("width: auto;");
//            sb.AppendLine("align-items: center;");
//            sb.AppendLine("background-color: white;");
//            cdkOverlayPane = sb.ToString();
//            _contextMenuVisible = true;
//        }

//        protected  void HandleDelete()
//        {
//            if (_currentModel == null)
//                return;
//            ShowDeleteDialogue();
//        }

//        private void Edit(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
//        {
//            if (_currentModel == null)
//                return;
//            _editMode = true;
//        }

//        private void EditRecord(T currentModel)
//        {
//            _currentModel = currentModel;
//            _editMode = true;
//        }


//        protected async Task HandleClose()
//        {
//            _editMode = false;
//            await InvokeAsync(StateHasChanged);
//        }


//        protected async Task HandleRefresh()
//        {
//            if (gridView == null)
//                return;
//            await gridView.RefreshDataAsync();
//        }

//        protected async Task HandleSearchFromRibbon(string searchTerm)
//        {
//            if (string.IsNullOrEmpty(searchTerm) || gridView == null)
//                return;
//            _searchTerm = searchTerm;
//            await gridView.RefreshDataAsync();
//        }
//        protected Dictionary<string, string> AddSearchTerms()
//        {
//            Dictionary<string, string> searchTerms = new();
//            _searchModels.Where(x => !string.IsNullOrEmpty(x.FieldName) && !string.IsNullOrEmpty(x.Value))
//                .ToList()
//                .ForEach(x => searchTerms.Add(x.FieldName, x.Value));
//            return searchTerms;
//        }
//    }
//}
