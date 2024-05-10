using System.Text;

namespace Phoenix.Models.Shared
{
	public class SearchModel
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public ConnectorType Connector { get; set; }
        public string Value { get; set; } = string.Empty;
        public bool FieldSelected { get; set; } = false;
        public Type FieldType { get; set; } = typeof(string);
	}

    public enum ConnectorType
    {
        And = 0,
        Or = 1
    }
}
