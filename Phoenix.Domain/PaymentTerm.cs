using System.ComponentModel.DataAnnotations;
namespace Phoenix.Domain
{
	/// <summary>
	/// Payment Terms, including calculations
	/// </summary>
	public class PaymentTerm : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(10)] public string PaymentDays { get; set; } = string.Empty;
	
	}
}
