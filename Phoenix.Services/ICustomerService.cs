
namespace Phoenix.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel?> AddCustomer(CustomerModel customerModel);
        Task<List<CustomerModel>> FindCustomer(Dictionary<string, string> searchTerms, string genericSearch, string sortBy, int skip = 0, int take = 0);
        Task<List<CustomerModel>> GetAllCustomers();
        Task<CustomerModel?> GetCustomerById(int id);
        Task<CustomerModel?> UpdateCustomer(CustomerModel customerModel);
    }
}