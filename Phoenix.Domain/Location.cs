using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class Location : BaseEntity
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }    
        public Warehouse Warehouse { get; set; }
        [Required, MaxLength(8)] public string LocationCode { get; set; } = string.Empty;
        [Required, MaxLength(40)] public string Name { get; set; } = string.Empty;
        public int? Length { get; set; }
        public int? Width { get; set; } 
        public int? Height { get; set; }
    }
}