using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Services
{
	/// <summary>
	/// This is a test for master detail
	/// </summary>
	public class SampleService : GenericService<SampleDetail, SampleHeaderModel>, 
		ISampleService, IGenericService<SampleDetail, SampleHeaderModel>
	{

		public SampleService(
			ILogger<BaseService> logger,
			IRepositoryBase<SampleDetail> repository)
			: base(logger, repository)
		{
		}

		public async Task<List<SampleHeaderModel>?> FindSamples(
			Dictionary<string, string> searchTerms,
			string genericSearch,
			Dictionary<string, byte> sortBy,
			int skip, int take)
		{
			//Need to join the Header and Details Tables for this example
			IQueryable<SampleDetail>? queryableObject = _repository.GetEntityReference() as IQueryable<SampleDetail>;
			queryableObject = queryableObject.Include(x => x.SampleHeader);
			return await FindRecords(queryableObject, searchTerms, genericSearch, sortBy, SimpleMapper.MapSampleHeaderToSampleHeaderModel!, skip, take);
		}



	}
}
