using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
    public class WarehouseModel: BaseModel
    {
        public int Id { get; set; }
        [Required, MaxLength(30)] public string Name { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string Street1 { get; set; } = string.Empty;
        [MaxLength(30)] public string Street2 { get; set; } = string.Empty;
        [Required, MaxLength(30)] public string City { get; set; } = string.Empty;
        [MaxLength(30)] public string County { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Postcode { get; set; } = string.Empty;
        [Required] public int CountryId { get; set; }
        public CountryModel? Country { get; }
        [Required, MaxLength(3)] public string CountryCode { get; set; } = string.Empty;
    }
}
