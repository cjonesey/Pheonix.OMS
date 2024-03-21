namespace Phoenix.WebClient.Components.Controls
{
    public partial class SearchWidget
    {
        protected List<SearchModel> searchModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            searchModel = new List<SearchModel>();
        }

        protected List<string> fieldNames = new List<string> { "ProductCode", "Description", "Size", "Date" };

        public void AddItem()
        {
            if (searchModel != null)
            {
                searchModel.Add(new SearchModel { Connector = ConnectorType.And, Id = Guid.NewGuid() });
            }
            StateHasChanged();
        }
        public void RemoveItem(SearchModel item)
        {
            searchModel.Remove(item);
            StateHasChanged();
        }
    }
}
