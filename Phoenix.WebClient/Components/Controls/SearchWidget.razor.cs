using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reflection;

namespace Phoenix.WebClient.Components.Controls
{
    public partial class SearchWidget
    {
        [Parameter] public EventCallback<List<SearchModel>> SearchInvoked { get; set; }
        [Parameter] public EventCallback ClearSearchInvoked { get; set; }

        [Parameter] public List<PropertyInfo> SearchProps 
        {
            get => _searchProps; 
            set => setSearchProps(value);
        }

		private bool _dateTimeFieldVisible = false;
        private DateTime newDate;
        private DateTime DateField 
        {
            get
            {
                return newDate;
            }
            set
            {
                newDate = value;
				var record = _searchModel.Find(x => x.Id == _currentRecord.Id);
                if (record == null)
                    return;

                if (string.IsNullOrEmpty(record.Value))
                {
                    record.Value = newDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    if (!record.Value.Contains(".."))
                        record.Value += "..";
                    record.Value = record.Value.Substring(0, record.Value.IndexOf("..") + 2) + newDate.ToString("dd/MM/yyyy");
                }
                _dateTimeFieldVisible = false;
                StateHasChanged();
            } 
        } 
        

        private List<SearchModel> _searchModel { get; set; }
        private bool AddFilterVisible { get; set; }
        private List<(string, string)> _lookupValues = new();
        private List<PropertyInfo> _searchProps = new List<PropertyInfo>();
        private SearchModel _currentRecord;

        protected override async Task OnInitializedAsync()
        {
            _searchModel = new List<SearchModel>();
            AddFilterVisible = true;
        }
       
        public void RemoveItem(SearchModel item)
        {
            _searchModel.Remove(item);
            StateHasChanged();
        }

        public void CheckFieldType(SearchModel item)
        {
            _currentRecord = item;
			if (item.FieldType == typeof(DateTime) || item.FieldType == typeof(Nullable<DateTime>))
            {
                _dateTimeFieldVisible = true;
            }
            else
            {
                _dateTimeFieldVisible = false;
            }
        }

        public void SetFieldName(ChangeEventArgs e)
        {
            if (e.Value == null) 
                return;    

            //The ID is stored as the GUID and the Name of the field
            var codes = e.Value.ToString()!.Split('|');
            if (codes.Length != 2) 
                return;

            //Get the field type from the searc props
            var searchProp = SearchProps.Where(x => x.Name == codes[1]);

			var model = _searchModel.Find(x => x.Id == Guid.Parse(codes[0]));
            if (model != null)
            {
                model.FieldName = codes[1];
                model.FieldSelected = true;
                model.FieldType = searchProp == null ? typeof(string) : searchProp.FirstOrDefault()!.PropertyType;
            }

            AddFilterVisible = true;
            StateHasChanged();
        }

        private void ClearFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            _searchModel = new List<SearchModel>();
            AddFilterVisible = true;
            ClearSearchInvoked.InvokeAsync();
        }
        private void AddFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            _searchModel.Add(new SearchModel { Connector = ConnectorType.And, Id = Guid.NewGuid() });
            AddFilterVisible = false;
            StateHasChanged();
        }

        private async Task ApplyFilter(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await SearchInvoked.InvokeAsync(this._searchModel);
        }

        private void setSearchProps(List<PropertyInfo> value)
        {
            _searchProps = value;
            _lookupValues.Clear();
            _lookupValues.Add(("Select", "Select"));
            _lookupValues.AddRange(_searchProps.Select(x => (x.Name, x.Name)).ToList());
        }
    }
}
