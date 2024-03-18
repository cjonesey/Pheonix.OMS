using System.ComponentModel.DataAnnotations;

namespace Phoenix.Domain
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        
        [Timestamp]
        public byte[] ChangeCheck { get; set; } = null!;
    }
}
