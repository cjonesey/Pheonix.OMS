using Phoenix.Domain;

namespace Phoenix.Models.Shared
{
	public class SampleDetailModel
	{
		public int Id { get; set; }
		public int SampleHeaderId { get; set; }
		public string Name { get; set; }
	}
}