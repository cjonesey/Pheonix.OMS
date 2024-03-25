using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Phoenix.Models.Shared
{
    public abstract class BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }        
        [Timestamp] public byte[] ChangeCheck { get; set; } = null!;
        public abstract object Identifier();
        public List<PropertyInfo> SearchProps() => GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(Searchable), true).Any()).ToList();
    }
}
