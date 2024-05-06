namespace Phoenix.Models.Shared
{
	/// <summary>
	/// Payment Terms, including calculations
	/// </summary>
	public class PaymentTermModel :  BaseModel
    { 
        public int Id { get; set; }
		[Searchable(SearchType = BaseValues.SearchType.StartsWith), Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
		[Searchable(SearchType = BaseValues.SearchType.Contains), Required, MaxLength(10)] public string PaymentDays { get; set; } = string.Empty;
        public override object Identifier() => Id;
		public override string NameField() => nameof(Name);
	}
}
