using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        
        [Timestamp]
        public byte[] ChangeCheck { get; set; } = null!;
    }
}
