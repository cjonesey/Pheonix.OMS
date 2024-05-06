using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Phoenix.WebClient.Components.Base
{
    public abstract partial class ListPageTemplate<T> : ComponentBase where T : BaseModel
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

        //Define the model for the searching
        protected T? _model;
        protected T? _currentModel;
        //Number of records to display - if required
        protected int numResults;

        //Grid Operation
        protected GridItemsProvider<T>? gridProvider;
        protected QuickGrid<T>? gridView;

        //Search Widgets
        protected List<SearchModel> _searchModels = new();
        protected string _searchTerm = string.Empty;
        protected string cdkOverlayPane;
        protected string sortBy = string.Empty;

        protected int _pageSize = 50;
        protected int _maxRecords = 5000;
        protected int _recordsLoaded = 0;
        protected bool _navigationRequired = true;
        protected List<T> _ModelValues = new List<T>();

        //Sort Model
        protected Dictionary<string, byte> _sortModel = new(); 

        protected void SetCurrentRow(T model)
        {
            _currentModel = model;
        }
        protected bool _editMode = false;

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

        protected void ContextMenu(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
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

        protected void HandleDelete()
        {
            if (_currentModel == null)
                return;
            ShowDeleteDialogue();
        }

        protected void Edit(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            if (_currentModel == null)
                return;
            _editMode = true;
        }

        protected void EditRecord(T currentModel)
        {
            _currentModel = currentModel;
            _editMode = true;
        }

        protected async Task HandleClose()
        {
            _editMode = false;
            await InvokeAsync(StateHasChanged);
        }

        protected async Task HandleRefresh()
        {
            if (gridView == null)
                return;
            await gridView.RefreshDataAsync();
        }

        protected async Task HandleSearchFromRibbon(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || gridView == null)
                return;
            _searchTerm = searchTerm;
            await gridView.RefreshDataAsync();
        }

        protected Dictionary<string, string> AddSearchTerms()
        {
            Dictionary<string, string> searchTerms = new();
            _searchModels.Where(x => !string.IsNullOrEmpty(x.FieldName) && !string.IsNullOrEmpty(x.Value))
                .ToList()
                .ForEach(x => searchTerms.Add(x.FieldName, x.Value));
            return searchTerms;
        }
        protected async Task ApplySearchFromWidget(List<SearchModel> searchModels)
        {
            if (searchModels == null || !searchModels.Any() || gridView == null)
                return;
            _searchModels = searchModels;
            await gridView!.RefreshDataAsync();
        }

        protected async Task ClearSearchFromWidget()
        {
            _searchModels.Clear();
            _searchTerm = string.Empty;
            if (gridView != null)
            {
                await gridView.RefreshDataAsync();
                StateHasChanged();
            }
        }
        protected async Task HandleSearchFromRibbonToTable(string searchTerm)
        {
            if ((string.IsNullOrEmpty(_searchTerm) && !string.IsNullOrEmpty(searchTerm))
                || (!string.IsNullOrEmpty(_searchTerm) && string.IsNullOrEmpty(searchTerm))
                || _searchTerm != searchTerm)
            {
				_searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm;
                await ApplyServerSearch();
			}
		}

        protected async Task ApplySearchFromWidgetToTable(List<SearchModel> searchModels)
        {
            if (searchModels == null || !searchModels.Any())
                _searchModels.Clear();
            _searchModels = searchModels;
			await ApplyServerSearch();
		}

		protected async Task ClearSearchFromWidgetToTable()
		{
			_searchModels.Clear();
			_searchTerm = string.Empty;
			await ApplyServerSearch();

		}

		private async Task ApplyServerSearch()
		{
			_ModelValues.Clear();
			_recordsLoaded = 0;
			_navigationRequired = true;
			await LoadData();
		}

		public abstract Task LoadData();

        protected async void SortTable(string sortColumn)
        {
            if (!_sortModel.ContainsKey(sortColumn))
            {
                _sortModel.Add(sortColumn, 1);
            }
            else
            {
                _sortModel[sortColumn]++;
                if (_sortModel[sortColumn] > 2)
                {
                    _sortModel[sortColumn] = 0;
                }
            }
			_navigationRequired = true;
			_recordsLoaded = 0;
			_ModelValues.Clear();
			await LoadData();
			StateHasChanged();
		}

		protected string SortVisible(string sortColumn)
        {
			
			if (!_sortModel.ContainsKey(sortColumn))
            {
                _sortModel.Add(sortColumn, 0);
            }
            switch(_sortModel[sortColumn])
            {
                case 1:
                    return "bi bi-arrow-down";
                case 2:
                    return "bi bi-arrow-up";
                default:
                    return "bi bi-arrow-down-up";
			}
		}
	}
}
