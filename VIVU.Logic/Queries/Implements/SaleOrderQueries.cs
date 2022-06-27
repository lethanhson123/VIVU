
namespace VIVU.Logic.Queries.Implements;

public class SaleOrderQueries : ISaleOrderQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public SaleOrderQueries(AppDatabase database,
        IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<SalesOrderModelResponse> Get()
    {
        return database.SalesOrders.Where(x => x.IsDeleted != true)
         .Select(x => mapper.Map<SalesOrderModelResponse>(x));
    }
    public IEnumerable<SalesOrderModelResponse> Get(SalesOrderQueryModel query)
    {
        return database.SalesOrders.Where(x => x.IsDeleted != true &&
                                         (x.Id.Contains(query.Keywords ?? string.Empty) ||
                                         x.Number.Contains(query.Keywords ?? string.Empty) ||
                                         x.CustomerId.Contains(query.Keywords ?? string.Empty) ||
                                         x.CustomerPhone.Contains(query.Keywords ?? string.Empty) ||
                                         x.CustomerEmail.Contains(query.Keywords ?? string.Empty)
                                         ))
          .Select(x => mapper.Map<SalesOrderModelResponse>(x)).Skip((query.PageIndex - 1) * query.Limit).Take(query.Limit);

    }
    public Task<SalesOrderModelResponse> GetDetail(string Id)
    {
        var data = new SalesOrderModelResponse();
        var dataDetail = new List<SalesOrderDetailModelResponse>();
        var order = database.SalesOrders.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted);
        var orderDetail = database.SalesOrderDetails.Where(x => x.SalesOrderId == Id && !x.IsDeleted).ToList();
        if (order != null)
        {
            mapper.Map(order, data);
        }
        if (orderDetail != null)
        {
            mapper.Map(orderDetail, dataDetail);
            data.OrderDetail = dataDetail;
        }

        return Task.FromResult(data);
    }
}
