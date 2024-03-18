using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class SalesInvoice : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(20)] public string InvoiceNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }  

    }
}
