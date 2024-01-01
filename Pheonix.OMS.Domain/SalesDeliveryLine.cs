using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class SalesDeliveryLine : BaseEntity
    {
        public int Id { get; set; }
        public int SalesDeliveryId { get; set; }
        public SalesDelivery SalesDelivery { get; set; }
        public int ItemId { get; set; } 
        public string ItemNumber { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string LocationCode { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public int QuantityShipped { get; set; } = 0;
        public int QuantityBackOrdered { get; set; } = 0;
    }
}
