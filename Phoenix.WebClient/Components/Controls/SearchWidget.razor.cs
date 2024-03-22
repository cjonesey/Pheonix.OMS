namespace Phoenix.WebClient.Components.Controls
{
    public partial class SearchWidget
    {
        private List<SearchModel> searchModel { get; set; }
        private bool AddFilterVisible { get; set; }

        protected override async Task OnInitializedAsync()
        {
            searchModel = new List<SearchModel>();
            AddFilterVisible = true;
        }

        protected List<string> fieldNames = new List<string> { "Please select", "ProductCode", "Description", "Size", "Date" };

        public void RemoveItem(SearchModel item)
        {
            searchModel.Remove(item);
            StateHasChanged();
        }

        public void SetFieldName(ChangeEventArgs e)
        {
            if (e.Value == null) 
                return;    
            var codes = e.Value.ToString()!.Split('|');
            if (codes.Length != 2) 
                return;
            var model = searchModel.Find(x => x.Id == Guid.Parse(codes[0]));
            if (model != null)
            {
                model.FieldName = codes[1];
                model.FieldSelected = true;
            }
            AddFilterVisible = true;
            StateHasChanged();
        }
        private void ClearFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            searchModel = new List<SearchModel>();
        }
        private void AddFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            searchModel.Add(new SearchModel { Connector = ConnectorType.And, Id = Guid.NewGuid() });
            AddFilterVisible = false;
            StateHasChanged();
        }
    }
}
