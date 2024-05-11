using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Phoenix.Shared;

namespace Phoenix.Services
{
	/// <summary>
	/// Basic Crud operation on PaymentServiceModel
	/// </summary>
	public class GenericService<T, U> : IGenericService<T, U> where T : BaseEntity
		where U : BaseModel
	{
		protected readonly ILogger<BaseService> _logger;
		protected readonly IRepositoryBase<T> _repository;

		public GenericService(ILogger<BaseService> logger,
			IRepositoryBase<T> repository)
		{
			_logger = logger;
			_repository = repository;
		}
		public async Task<List<U>> GetAll(Func<T, U> MapResults)
		{
			List<U> outValues = new List<U>();
			var values = await _repository.GetAll();
			if (values != null && values.Any())
			{
				values.ToList().ForEach(x => outValues.Add(MapResults(x)));
			}
			return outValues;
		}

		public async Task<U?> GetById(int id, Func<T, U> MapResults)
		{
			var model = await _repository.Get(id);
			if (model != null)
			{
				return MapResults(model);
			}
			return default(U);
		}


		/// <summary>
		/// FindRecords using the a generic repository
		/// </summary>
		/// <param name="searchTerms"></param>
		/// <param name="genericSearch"></param>
		/// <param name="sortBy"></param>
		/// <param name="MapResults"></param>
		/// <param name="skip"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		public async Task<List<U>?> FindRecords(
			Dictionary<string, string> searchTerms,
			string genericSearch,
			Dictionary<string, byte> sortBy,
			Func<T, U> MapResults, int skip, int take)
		{
			var queryableObject = _repository.GetEntityReference();
			return await FindRecords(queryableObject, searchTerms, genericSearch, sortBy, MapResults, skip, take);
		}


		/// <summary>
		/// Validates the search and sort records and adds the map to get the 
		/// </summary>
		/// <param name="queryableObject"></param>
		/// <param name="searchTerms"></param>
		/// <param name="genericSearch"></param>
		/// <param name="sortBy"></param>
		/// <param name="MapResults"></param>
		/// <param name="skip"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		public async Task<List<U>?> FindRecords(
			IQueryable<T> queryableObject,
			Dictionary<string, string> searchTerms,
			string genericSearch,
			Dictionary<string, byte> sortBy,
			Func<T, U> MapResults, int skip, int take)
		{
			var entityProps = typeof(T).GetProperties();
			var modelProps = typeof(U).GetProperties();
			List<T>? entityRecords = new();
			List<U> modelRecords = new();

			try
			{
				List<(string, string, Type, BaseValues.SearchType)> entitySearchTerms = new();
				if (searchTerms != null && searchTerms.Any())
				{
					foreach (var (key, value) in searchTerms.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
					{
						CheckAndUpdateSearchModel(entityProps, modelProps, entitySearchTerms, key, value);
					}
				}

				//This is for the generic search - we are only applying to a name field 
				//Should make this separate so we can override
				if (!string.IsNullOrEmpty(genericSearch))
				{
					var model = (U)Activator.CreateInstance(typeof(U));
					PropertyInfo? nameProp = entityProps.Where(x => x.Name == model!.NameField()).FirstOrDefault();
					if (nameProp != null)
					{
						entitySearchTerms.Add(new(nameProp.Name, genericSearch, nameProp.PropertyType, BaseValues.SearchType.Contains));
					}
				}

				//Create a new search object based on the 
				Dictionary<string, byte> sortBySorted = new Dictionary<string, byte>();
				foreach (var sortRecord in sortBy.Where(x => x.Value != 0).ToList())
				{
					CheckAndUpdateSortProperties(modelProps, sortBySorted, sortRecord);
				}


				if (take == 0) { take = 20; }
				entityRecords = await _repository.GetUsingGenericSearch(queryableObject, entitySearchTerms, sortBySorted, take, skip);

				if (entityRecords == null)
					entityRecords = new List<T>();

				entityRecords.ForEach(x => modelRecords.Add(MapResults(x)));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving the record from the database");
			}
			return modelRecords;
		}

		/// <summary>
		/// Check the search model is valid - invalid searches are removed (Where they are not in the base entity)
		/// </summary>
		/// <param name="entityProps"></param>
		/// <param name="modelProps"></param>
		/// <param name="entitySearchTerms"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		private static void CheckAndUpdateSearchModel(PropertyInfo[] entityProps, PropertyInfo[] modelProps, List<(string, string, Type, BaseValues.SearchType)> entitySearchTerms, string key, string value)
		{
			PropertyInfo? propSearchEntity = modelProps.Where(x => x.Name == key).FirstOrDefault();
			if (propSearchEntity == null)
				return;
			string fieldname = propSearchEntity!.GetFieldNameForProperty();
			BaseValues.SearchType matchType = propSearchEntity!.GetSearchTypeForProperty();
			PropertyInfo? prop = null;
			if (fieldname.Contains("."))
			{
				prop = PredicateGenericHelper.GetPropertiesRecursively(typeof(T), fieldname);
			}
			else
			{
				prop = entityProps.Where(x => x.Name == fieldname).FirstOrDefault();
			}
			if (prop == null)
				return;
			entitySearchTerms.Add(new(fieldname, value, prop.PropertyType, matchType));
		}

		/// <summary>
		/// Validate the sort properties
		/// </summary>
		/// <param name="modelProps"></param>
		/// <param name="sortBySorted"></param>
		/// <param name="sortRecord"></param>
		private static void CheckAndUpdateSortProperties(PropertyInfo[] modelProps, Dictionary<string, byte> sortBySorted, KeyValuePair<string, byte> sortRecord)
		{
			var prop = modelProps.Where(x => x.Name == sortRecord.Key).FirstOrDefault();
			if (prop == null)
				return;
			var fieldName = prop.GetFieldNameForProperty();
			prop = PredicateGenericHelper.GetPropertiesRecursively(typeof(T), fieldName);
			if (prop == null)
				return;
			sortBySorted.Add(fieldName, sortRecord.Value);
		}




		//public async Task<CountryModel?> AddCountry(CountryModel countryModel)
		//{
		//    var country = new Country
		//    {
		//        Code = countryModel.Code,
		//        ISOCode2 = countryModel.ISOCode2,
		//        Name = countryModel.Name,
		//        ModifiedOn = DateTime.Now,
		//        CreatedOn = DateTime.Now
		//    };
		//    await _repository.Add(country, country.Id);
		//    return await GetCountryById(country.Id);
		//}

		//public async Task<CountryModel?> UpdateCountry(CountryModel countryModel)
		//{
		//    var country = new Country
		//    {
		//        Id = countryModel.Id,
		//        Code = countryModel.Code,
		//        ISOCode2 = countryModel.ISOCode2,
		//        Name = countryModel.Name,
		//        CreatedOn = countryModel.CreatedOn,
		//        ModifiedOn = DateTime.Now,
		//        ChangeCheck = countryModel.ChangeCheck
		//    };
		//    await _repository.Update(country, country.Id);
		//    return await GetCountryById(country.Id);
		//}

		//public async Task<List<CountryModel>?> FindCountry(Dictionary<string, string> searchTerms, string genericSearch, string sortBy, int skip, int take)
		//{

	}
}
