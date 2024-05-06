namespace Phoenix.Models.Shared
{
	public class SearchModel
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public ConnectorType Connector { get; set; }
        public string Value { get; set; }
        public bool FieldSelected { get; set; } = false;
    }

    public enum ConnectorType
    {
        And = 0,
        Or = 1
    }
}
