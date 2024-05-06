using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Domain
{
    public class SalesDelivery : BaseEntity
    {
        public int Id { get; set; }
        public string SalesDeliveryNumber { get; set; } = string.Empty; 
        public int WarehouseId { get; set; }		
	}
}
