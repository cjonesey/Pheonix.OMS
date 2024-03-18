using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class Supplier : BaseEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(30)] public string SupplierCode { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string Street1 { get; set; } = string.Empty;
        [MaxLength(30)] public string Street2 { get; set; } = string.Empty;
        [MaxLength(30)] public string City { get; set; } = string.Empty;
        [MaxLength(30)] public string County { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Postcode { get; set; } = string.Empty;
        public int CountryId { get; set; }
        [Required, MaxLength(3)] public string CountryCode { get; set; } = string.Empty;
    }
}
