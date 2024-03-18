namespace Phoenix.Domain
{
    public class ItemWarehouse : BaseEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        //Need a Warehouse object here
        public int? LocationId  { get; set; } 
        public Location Location { get; set; }
        //Need a Location object here
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

    }
}
