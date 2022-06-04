namespace VIVU.Logic.Queries.Interfaces;

public interface ICustomerQueries
{
    IEnumerable<CustomerModel> Get();
    IEnumerable<CustomerModel> Get(CustomerQueryModel query);
    IEnumerable<CustomerModel> Get(string? keywords);
    Task<CustomerModel> GetDetail(int Id);
}
