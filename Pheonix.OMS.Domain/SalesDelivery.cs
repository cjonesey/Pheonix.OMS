using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class SalesDelivery : BaseEntity
    {
        public int Id { get; set; }
        public string SalesDeliveryNumber { get; set; } = string.Empty; 
        public int WarehouseId { get; set; }
    }
}
