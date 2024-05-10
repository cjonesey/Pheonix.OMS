namespace Phoenix.Shared
{
	public class Searchable : Attribute
    {
        public BaseValues.SearchType SearchType { get; set; }
		public string Fieldname { get; set; }
	}

    public static class BaseValues
    {
        public enum SearchType
        {
            Contains,
            StartsWith,
            EndsWith,
            Equals,
            GreaterThan,
            GreaterThanOrEqual,
            LessThan,
            LessThanOrEqual
        }
    }
}
