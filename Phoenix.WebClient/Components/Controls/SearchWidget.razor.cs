using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Reflection;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class SearchWidget
    {
        [Parameter] public EventCallback<List<SearchModel>> SearchInvoked { get; set; }
        [Parameter] public EventCallback ClearSearchInvoked { get; set; }

        [Parameter] public List<PropertyInfo> SearchProps 
        {
            get => searchProps; 
            set => setSearchProps(value);
        }



        private List<SearchModel> searchModel { get; set; }
        private bool AddFilterVisible { get; set; }
        private List<(string, string)> lookupValues = new();
        private List<PropertyInfo> searchProps = new List<PropertyInfo>();

        protected override async Task OnInitializedAsync()
        {
            searchModel = new List<SearchModel>();
            AddFilterVisible = true;
        }
       
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
            AddFilterVisible = true;
            ClearSearchInvoked.InvokeAsync();
        }
        private void AddFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            searchModel.Add(new SearchModel { Connector = ConnectorType.And, Id = Guid.NewGuid() });
            AddFilterVisible = false;
            StateHasChanged();
        }

        private async Task ApplyFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await SearchInvoked.InvokeAsync(this.searchModel);
        }

        private void setSearchProps(List<PropertyInfo> value)
        {
            searchProps = value;
            lookupValues.Clear();
            lookupValues.Add(("Select", "Select"));
            lookupValues.AddRange(searchProps.Select(x => (x.Name, x.Name)).ToList());
        }
    }
}
