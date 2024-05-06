namespace Phoenix.Models.Shared
{
	public class CustomerPostingGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
