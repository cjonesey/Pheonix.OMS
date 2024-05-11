using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Domain
{
	public class SampleHeader: BaseEntity
	{
        public int Id { get; set; }
		public string Code { get; set; }
		public virtual IList<SampleDetail>SampleDetails { get; set; }

	}
}
