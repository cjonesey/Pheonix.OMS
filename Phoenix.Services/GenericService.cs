using Phoenix.Shared;

namespace Phoenix.Services
{
	/// <summary>
	/// Basic Crud operation on PaymentServiceModel
	/// </summary>
	public class GenericService<T, U> : IGenericService<T, U> where T : BaseEntity
        where U : BaseModel
    {
        public ILogger<GenericService<T, U>> _logger;
        private readonly IRepositoryBase<T> _repository;

        public GenericService(ILogger<GenericService<T, U>> logger,
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

		public async Task<List<U>?> FindRecords(
			Dictionary<string, string> searchTerms,
			string genericSearch,
			Dictionary<string, byte> sortBy,
			Func<T, U> MapResults, int skip, int take)
		{
			var entityProps = typeof(T).GetProperties();
			var modelProps = typeof(U).GetProperties();
			List<T>? entityRecords = new();
			List<U> modelRecords = new();
			//List<PropertyInfo> differences = new List<PropertyInfo>();
			//Expression<Func<T, bool>>? condition = default;
			var predicate = PredicateBuilder.True<T>();
			bool predicateApplied = false;

			try
			{
				List<(string, string, Type, BaseValues.SearchType)> entitySearchTerms = new();
				if (searchTerms != null && searchTerms.Any())
				{
					foreach (var (key, value) in searchTerms.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
					{
						PropertyInfo? propSearchEntity = modelProps.Where(x => x.Name == key).FirstOrDefault();
						if (propSearchEntity == null)
							break;

						string fieldname = propSearchEntity!.GetFieldNameForProperty();
						BaseValues.SearchType matchType = propSearchEntity!.GetSearchTypeForProperty();



						PropertyInfo? prop = entityProps.Where(x => x.Name == fieldname).FirstOrDefault();
						if (prop == null)
							break;
						entitySearchTerms.Add(new(fieldname, value, prop.PropertyType, matchType));

						if (!string.IsNullOrEmpty(genericSearch))
						{
							var model = (U)Activator.CreateInstance(typeof(U));
							PropertyInfo? nameProp = entityProps.Where(x => x.Name == model!.NameField()).FirstOrDefault();
							if (nameProp != null)
							{
								entitySearchTerms.Add(new(nameProp.Name, genericSearch, nameProp.PropertyType, matchType));
							}
						}

						////Check whether the value contains the | character - only works for equals
						//if (value.Contains('|') && matchType == BaseValues.SearchType.Equals)
						//{
						//	condition = PredicateGenericHelper.CreateExpressionCallFromList<T>(key, value, prop);
						//}
						//else
						//{
						//	PropertyInfo? propSearch = modelProps.Where(x => x.Name == key).FirstOrDefault();
						//	condition = PredicateGenericHelper.CreateExpressionCall<T>(
						//		key,
						//		value,
						//		PredicateGenericHelper.GetMethod(prop.PropertyType, matchType),
						//		prop.PropertyType);
						//}
						//predicate = predicate.And(condition!);
						//predicateApplied = true;
					}
				}

				if (take == 0) { take = 20; }
				entityRecords = await _repository.GetExpanded(entitySearchTerms, sortBy, take, skip);

				if (entityRecords == null)
					entityRecords = new List<T>();

				entityRecords.ForEach(x => modelRecords.Add(MapResults(x)));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in FindWarehouse");
			}
			return modelRecords;
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
