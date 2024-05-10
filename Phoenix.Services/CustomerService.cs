using Microsoft.EntityFrameworkCore;
using Phoenix.Domain;
using Phoenix.Shared;

namespace Phoenix.Services
{
    /// <summary>
    /// Service for managing customers, standard CRUD operations using the CustomerModel and 
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private ILogger<CustomerService> _logger;
        private IRepositoryBase<Customer> _repository;

        public CustomerService(ILogger<CustomerService> logger,
            IRepositoryBase<Customer> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            List<CustomerModel> customerModels = new();
            var customers = await _repository.GetAll();
            if (customers != null && customers.Any())
            {
                customers.ToList().ForEach(x => customerModels.Add(MapCustomer(x)));
            }
            return customerModels;
        }

        /// <summary>
        /// Get Customer by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerModel?> GetCustomerById(int id)
        {
            var customer = await _repository.Get(id);
            if (customer != null)
            {
                return MapCustomer(customer);
            }
            return default;
        }


        /// <summary>
        /// Add customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public async Task<CustomerModel?> AddCustomer(CustomerModel customerModel)
        {
            var customer = new Customer
            {
                Name = customerModel.Name,
                Street1 = customerModel.Street1,
                Street2 = customerModel.Street2,
                City = customerModel.City,
                County = customerModel.County,
                Postcode = customerModel.Postcode,
                CountryId = customerModel.CountryId,
                CountryCode = customerModel.CountryCode,
                Phone = customerModel.Phone,
                Email = customerModel.Email,
                Website = customerModel.Website,
                ContactPerson = customerModel.ContactPerson,
                VATRegistrationNumber = customerModel.VATRegistrationNumber,
                GLN = customerModel.GLN,
                CompanyRegistrationNumber = customerModel.CompanyRegistrationNumber,
                EORIRegistrationNumber = customerModel.EORIRegistrationNumber,
                VATPostingGroupID = customerModel.VATPostingGroup.Id,
                CustomerPostingGroupID = customerModel.CustomerPostingGroup.Id,
                CustomerPriceGroupID = customerModel.CustomerPriceGroup.Id,
                ConsolidateInvoices = customerModel.ConsolidateInvoices,
                PrepaymentPercentage = customerModel.PrepaymentPercentage,
                CreditLimit = customerModel.CreditLimit,
                CashApplicationMethod = customerModel.CashApplicationMethod,
                PaymentTermsID = customerModel.PaymentTerms.Id,
                ReserveStock = customerModel.ReserveStock,
                ShipComplete = customerModel.ShipComplete,
                ShowPricesOnSalesDocuments = customerModel.ShowPricesOnSalesDocuments
            };
            await _repository.Add(customer, customer.Id);
            return await GetCustomerById(customer.Id);
        }

        public async Task<CustomerModel?> UpdateCustomer(CustomerModel customerModel)
        {
            var customer = new Customer
            {
                Id = customerModel.Id,
                Name = customerModel.Name,
                Street1 = customerModel.Street1,
                Street2 = customerModel.Street2,
                City = customerModel.City,
                County = customerModel.County,
                Postcode = customerModel.Postcode,
                CountryId = customerModel.CountryId,
                CountryCode = customerModel.CountryCode,
                Phone = customerModel.Phone,
                Email = customerModel.Email,
                Website = customerModel.Website,
                ContactPerson = customerModel.ContactPerson,
                VATRegistrationNumber = customerModel.VATRegistrationNumber,
                GLN = customerModel.GLN,
                CompanyRegistrationNumber = customerModel.CompanyRegistrationNumber,
                EORIRegistrationNumber = customerModel.EORIRegistrationNumber,
                CustomerPostingGroupID = customerModel.VATPostingGroup.Id,
                CustomerPriceGroupID = customerModel.CustomerPriceGroup.Id,
                ConsolidateInvoices = customerModel.ConsolidateInvoices,
                PrepaymentPercentage = customerModel.PrepaymentPercentage,
                CreditLimit = customerModel.CreditLimit,
                CashApplicationMethod = customerModel.CashApplicationMethod,
                PaymentTermsID = customerModel.PaymentTerms.Id,
                ReserveStock = customerModel.ReserveStock,
                ShipComplete = customerModel.ShipComplete,
                ShowPricesOnSalesDocuments = customerModel.ShowPricesOnSalesDocuments
            };
            await _repository.Update(customer, customer.Id);
            return await GetCustomerById(customer.Id);
        }

        private static CustomerModel MapCustomer(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Street1 = customer.Street1,
                Street2 = customer.Street2,
                City = customer.City,
                County = customer.County,
                Postcode = customer.Postcode,
                CountryId = customer.CountryId,
                CountryCode = customer.CountryCode,
                Phone = customer.Phone,
                Email = customer.Email,
                Website = customer.Website,
                ContactPerson = customer.ContactPerson,
                VATRegistrationNumber = customer.VATRegistrationNumber,
                GLN = customer.GLN,
                CompanyRegistrationNumber = customer.CompanyRegistrationNumber,
                EORIRegistrationNumber = customer.EORIRegistrationNumber,
                //VATPostingGroup = customer.VATPostingGroup,
                //CustomerPostingGroup = customer.CustomerPostingGroup,
                //CustomerPriceGroup = customer.CustomerPriceGroup,
                ConsolidateInvoices = customer.ConsolidateInvoices,
                PrepaymentPercentage = customer.PrepaymentPercentage,
                CreditLimit = customer.CreditLimit,
                CashApplicationMethod = customer.CashApplicationMethod,
                //PaymentTerms = customer.PaymentTerms,
                ReserveStock = customer.ReserveStock,
                ShipComplete = customer.ShipComplete,
                ShowPricesOnSalesDocuments = customer.ShowPricesOnSalesDocuments
            };
        }

        public async Task<List<CustomerModel>> FindCustomer(Dictionary<string, string> searchTerms,
            string genericSearch,
            string sortBy,
            int skip = 0,
            int take = 0)
        {
            var predicate = PredicateBuilder.True<Customer>();
            var customerProps = typeof(Customer).GetProperties();
            var warehouseSearchProps = typeof(CustomerModel).GetProperties();
            List<Customer>? customers = new();
            List<CustomerModel> customerModels = new();
            List<PropertyInfo> differences = new List<PropertyInfo>();
            Expression<Func<Customer, bool>> condition = default;
            bool predicateApplied = false;

            try
            {
                if (searchTerms != null && searchTerms.Any())
                {
                    foreach (var (key, value) in searchTerms
                        .Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)).ToList())
                    {
                        //Check that the value is in the warehouse entity
                        PropertyInfo? prop = customerProps.Where(x => x.Name == key).FirstOrDefault();
                        if (prop == null)
                        {
                            throw new Exception("Property does not exist");
                        }

                        PropertyInfo? propSearch1 = warehouseSearchProps.Where(x => x.Name == key).FirstOrDefault();
                        BaseValues.SearchType matchType = ModelAttributeHelpers.GetSearchTypeForObject(propSearch1);

                        //Check whether the value contains the | character - only works for equals
                        if (value.Contains('|') && matchType == BaseValues.SearchType.Equals)
                        {
                            condition = PredicateGenericHelper.CreateExpressionCallFromList<Customer>(key, value, prop.PropertyType);
                        }
                        else
                        {
                            PropertyInfo? propSearch = warehouseSearchProps.Where(x => x.Name == key).FirstOrDefault();
                            condition = PredicateGenericHelper.CreateExpressionCall<Customer>(
                                key,
                                value,
                                PredicateGenericHelper.GetMethod(prop.PropertyType, matchType),
                                prop.PropertyType);
                        }
                        predicate = predicate.And(condition!);
                        predicateApplied = true;
                    }
                }
                if (!string.IsNullOrEmpty(genericSearch))
                {
                    predicate = predicate.And(x => x.Name.Contains(genericSearch));
                    predicateApplied = true;
                }

                if (take == 0) { take = 20; }

                List<Customer>? result = null;
                if (predicateApplied)
                {
                    result = await _repository.Get(predicate, x => x.OrderByDescending(x => x.Id), take, skip);
                }
                else
                {
                    result = await _repository.Get(null, null, take, skip);
                }

                if (result == null)
                    result = new List<Customer>();

                result.ForEach(x => customerModels.Add(MapCustomer(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Find Customers");
            }
            return customerModels;
        }




    }
}
