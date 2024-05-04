using Phoenix.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
	/// <summary>
	/// Payment Terms, including calculations
	/// </summary>
	public class PaymentTermModel :  BaseModel
    { 
        public int Id { get; set; }
        [Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(10)] public string PaymentDays { get; set; } = string.Empty;
        public override object Identifier() => Id;
    }
}
