using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
    public class WarehouseModel: BaseModel
    {
        public int Id { get; set; }
        [Required, MaxLength(30), Description("Name"), Searchable(SearchType = BaseValues.SearchType.Contains)] public string Name { get; set; } = string.Empty;        
        [Required, MaxLength(30), Description("Street 1"), Searchable(SearchType = BaseValues.SearchType.Contains)]  public string Street1 { get; set; } = string.Empty;        
        [MaxLength(30), Description("Street 2"), Searchable(SearchType = BaseValues.SearchType.Contains)] public string Street2 { get; set; } = string.Empty;
        [Required, MaxLength(30), Searchable(SearchType = BaseValues.SearchType.Contains)] public string City { get; set; } = string.Empty;        
        [MaxLength(30)] public string County { get; set; } = string.Empty;
        [Required, MaxLength(20), Searchable(SearchType = BaseValues.SearchType.StartsWith)] public string Postcode { get; set; } = string.Empty;        
        [Required] public int CountryId { get; set; }
        public CountryModel? Country { get; }
        [Required, MaxLength(3),Searchable(SearchType = BaseValues.SearchType.Equals)] public string CountryCode { get; set; } = string.Empty;
        public override object Identifier() => Id;
    }
}
