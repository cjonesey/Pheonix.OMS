
namespace Phoenix.Services
{
	public interface ISampleService
	{
		Task<List<SampleHeaderModel>?> FindSamples(Dictionary<string, string> searchTerms, string genericSearch, Dictionary<string, byte> sortBy, int skip, int take);
	}
}