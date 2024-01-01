using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pheonix.OMS.Domain
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        
        [Timestamp]
        public byte[] ChangeCheck { get; set; } = null!;
    }
}
