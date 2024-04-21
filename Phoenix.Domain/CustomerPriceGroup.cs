namespace Phoenix.Domain
{
    public class CustomerPriceGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
