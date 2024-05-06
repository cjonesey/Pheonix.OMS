using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Domain
{
    public class PaymentMethod : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
	}
}
