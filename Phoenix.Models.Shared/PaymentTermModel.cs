namespace Phoenix.Models.Shared
{
	/// <summary>
	/// Payment Terms, including calculations
	/// </summary>
	public class PaymentTermModel :  BaseModel
    { 
        public int Id { get; set; }
		[Searchable(SearchType = BaseValues.SearchType.Equals), Required, MaxLength(10)] public string Code { get; set; } = string.Empty;
		[Searchable(SearchType = BaseValues.SearchType.StartsWith), Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
		[Searchable(SearchType = BaseValues.SearchType.Contains), Required, MaxLength(10)] public string PaymentDayCalculation { get; set; } = string.Empty;
		[Searchable(SearchType = BaseValues.SearchType.Equals)] public DateTime PaymentDate { get; set; }		
		[Searchable(SearchType = BaseValues.SearchType.Equals), Required] public int PaymentDay { get; set; }
		public override object Identifier() => Id;
		public override string NameField() => nameof(Name);
		[Searchable(SearchType = BaseValues.SearchType.Equals)] public DateTime? NullableDate { get; set; }
		[Searchable(SearchType = BaseValues.SearchType.Equals)] public int? NullableInt { get; set; }
		[Searchable(SearchType = BaseValues.SearchType.Equals)] public decimal? NullableDecimal { get; set; }

	}
}
