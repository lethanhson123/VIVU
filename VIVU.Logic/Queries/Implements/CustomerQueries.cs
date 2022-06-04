
namespace VIVU.Logic.Queries.Implements;

public class CustomerQueries : ICustomerQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public CustomerQueries(AppDatabase database,
        IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<CustomerModel> Get()
    {
        return database.Customers.Where(x => x.IsDeleted != true)
         .Select(x => mapper.Map<CustomerModel>(x));
    }
    public IEnumerable<CustomerModel> Get(CustomerQueryModel query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CustomerModel> Get(string? keywords)
    {
        return database.Customers.Where(x => x.IsDeleted != true &&
                                          (x.Phone.Contains(keywords ?? string.Empty) ||
                                          x.Email.Contains(keywords ?? string.Empty) ||
                                          x.FullName.Contains(keywords ?? string.Empty) ||
                                          x.IdentityNumber.Contains(keywords ?? string.Empty)))
           .Select(x => mapper.Map<CustomerModel>(x));
    }

    public Task<CustomerModel> GetDetail(int Id)
    {
        var data = new CustomerModel();

        var hexagram = database.Products.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        if (hexagram != null)
        {
            mapper.Map(hexagram, data);
        }

        return Task.FromResult(data);
    }
}
