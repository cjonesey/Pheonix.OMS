using Pheonix.OMS.Domain.SubDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class SalesInvoice : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(20)] public string InvoiceNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }  

    }
}
