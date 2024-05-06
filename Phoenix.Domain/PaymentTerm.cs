using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
