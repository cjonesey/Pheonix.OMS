using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Domain
{
	public class SampleDetail : BaseEntity
	{
        public int Id { get; set; }
		public SampleHeader SampleHeader { get; set; }
		public int SampleHeaderId { get; set; }
		public string Name { get; set; }
    }
}
