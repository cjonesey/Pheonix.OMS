namespace Phoenix.Shared
{
	public static class ModelAttributeHelpers
	{
		public static BaseValues.SearchType GetSearchTypeForObject(PropertyInfo? prop)
		{
			BaseValues.SearchType searchType = BaseValues.SearchType.Equals;
			if (prop == null)
				return searchType;

			var attributes = prop.GetCustomAttributes(typeof(Searchable), false);
			if (attributes.Length > 0)
			{
				foreach (var att in attributes)
				{
					if (att is Searchable)
					{
						searchType = ((Searchable)att).SearchType;
					}
				}
			}
			return searchType;
		}


		public static BaseValues.SearchType GetSearchTypeForProperty(this PropertyInfo prop)
		{
			BaseValues.SearchType searchType = BaseValues.SearchType.Equals;
			if (prop == null)
				return searchType;

			var attributes = prop.GetCustomAttributes(typeof(Searchable), false);
			if (attributes.Length > 0)
			{
				foreach (var att in attributes)
				{
					if (att is Searchable)
					{
						searchType = ((Searchable)att).SearchType;
					}
				}
			}
			return searchType;
		}

		public static string GetFieldNameForProperty(this PropertyInfo prop)
		{
			var attributes = prop.GetCustomAttributes(typeof(FieldBase), false);
			if (attributes.Length > 0)
			{
				foreach (var att in attributes)
				{
					if (att is FieldBase)
					{
						return ((FieldBase)att).Name ?? prop.Name;
					}
				}
			}
			return prop.Name;
		}
	}
}
