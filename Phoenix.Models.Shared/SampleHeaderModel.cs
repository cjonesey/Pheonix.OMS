using Phoenix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models.Shared
{
	public class SampleHeaderModel : BaseModel
	{
		[Searchable(BaseValues.SearchType.Equals)]
		[FieldBase("SampleHeader.Id")]
		[Description("Header Id")]
		[Required] 
		public int Id { get; set; }
		
		[Searchable(BaseValues.SearchType.Equals)]
		[FieldBase("SampleHeader.Code")]
		[Required] 
		public string Code { get; set; }
		
		[Searchable(BaseValues.SearchType.Equals)]
		[Description("Detail Id")]
		[FieldBase("Id")]
		[Required] 
		public int DetailId { get; set; }

		[Searchable(BaseValues.SearchType.StartsWith)]
		[Required] 
		public string Name { get; set; } = string.Empty;

		public override object Identifier() => this.Id;
		public override string NameField() => nameof(this.Code);
	}
}
