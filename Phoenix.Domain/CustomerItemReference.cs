using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class CustomerItemReference : BaseEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();
        [Required] public int ItemId { get; set; }
        public Item Item { get; set; } = new Item();
        [Required, MaxLength(20)] public string CustomerItemCode { get; set; } = string.Empty;
    }
}
