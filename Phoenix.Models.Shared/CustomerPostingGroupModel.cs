using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Domain
{
    public class CustomerPostingGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
