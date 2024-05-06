using System.ComponentModel.DataAnnotations;

namespace Phoenix.Models.Shared
{
    public class CountryModel : BaseModel
    {
        public int Id { get; set; }
        [Searchable(SearchType = BaseValues.SearchType.Equals), Required, MaxLength(3)] public string Code { get; set; } = string.Empty;
        [Searchable(SearchType = BaseValues.SearchType.Equals), Required, MaxLength(2)] public string ISOCode2 { get; set; } = string.Empty;
        [Searchable(SearchType = BaseValues.SearchType.Contains)] public string Name { get; set; } = string.Empty;
        public virtual IList<WarehouseModel> Warehouses { get; set; } = new List<WarehouseModel>();
        public override object Identifier() => Id;
		public override string NameField() => nameof(Name);

	}
}
