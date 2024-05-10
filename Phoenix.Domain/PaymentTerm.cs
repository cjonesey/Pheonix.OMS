using System.ComponentModel.DataAnnotations;
namespace Phoenix.Domain
{
	/// <summary>
	/// Payment Terms, including calculations
	/// </summary>
	public class PaymentTerm : BaseEntity
    {
        public int Id { get; set; }
		[Required, MaxLength(10)] public string Code { get; set; } = string.Empty;
		[Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(10)] public string PaymentDayCalculation { get; set; } = string.Empty;
		[Required] public int PaymentDay { get; set; } = 0;
		[Required] public DateTime PaymentDate { get; set; }
		public DateTime? NullableDate { get; set; }
		public int? NullableInt { get; set; }
		public decimal? NullableDecimal { get; set; }
		//Add in an Enum ToDo:

	}
}
