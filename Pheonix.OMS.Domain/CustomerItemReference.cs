using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class CustomerItemReference : BaseEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();
        [Required] public int ItemId { get; set; }
        public Item Item { get; set; } = new Item();
        [Required, MaxLength(20)] public string CustomerItemCode { get; set; } = string.Empty;
    }
}
