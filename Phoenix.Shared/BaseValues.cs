namespace Phoenix.Shared
{
	public class Searchable : Attribute
    {
        public BaseValues.SearchType SearchType { get; set; } = BaseValues.SearchType.Equals;

		public Searchable(BaseValues.SearchType searchType)
		{
			SearchType = searchType;
		}
        public Searchable()
        {            
        }
    }

	public class FieldBase : Attribute
	{
		public string Name { get; set; } = string.Empty;
        public FieldBase(string name)
        {
            Name = name;
        }
        public FieldBase() {}
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
