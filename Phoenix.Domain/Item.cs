using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class Item : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(20)] public string ItemCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(60)] public string Description { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string SupplierCode { get; set; } = string.Empty;
        [Precision(18,2)] public decimal SellPrice { get; set; }
        [Precision(18, 2)] public decimal CostPrice { get; set; }
        public int VATProductId { get; set; }
        public string VATCode { get; set; } = string.Empty;

    }
}
