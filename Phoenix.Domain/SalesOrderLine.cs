using Microsoft.EntityFrameworkCore;

namespace Phoenix.Domain
{
    public class SalesOrderLine : BaseEntity
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int ItemId { get; set; } 
        public Item Item { get; set; }
        public int Quantity { get; set; } = 0;
        public int QuantityAllocated { get; set; } = 0;
        public int QuantityShipped { get; set; } = 0;
        public int QuantityBackOrdered { get; set; } = 0;
        public int QuantityReturned { get; set; } = 0;
        public int QuantityInvoiced { get; set; } = 0;
        [Precision(18, 2)] public decimal UnitPrice { get; set; } = 0;
        [Precision(18, 2)] public decimal UnitCost { get; set; } = 0;
        [Precision(18, 2)] public decimal LineTotal { get; set; } = 0;
        [Precision(18, 2)] public decimal Discount { get; set; } = 0;
        [Precision(18, 2)] public decimal Tax { get; set; } = 0;
    }
}
