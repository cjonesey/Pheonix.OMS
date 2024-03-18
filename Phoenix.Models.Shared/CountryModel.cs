using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
    public class CountryModel : BaseModel
    {
        public int Id { get; set; }
        [Required, MaxLength(3)] public string Code { get; set; } = string.Empty;
        [Required, MaxLength(2)] public string ISOCode2 { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public virtual IList<WarehouseModel> Warehouses { get; set; } = new List<WarehouseModel>();
    }
}
