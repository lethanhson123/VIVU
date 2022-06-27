namespace VIVU.Logic.Queries.Implements;

public class ProductQueries : IProductQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public ProductQueries(AppDatabase database,
        IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<ProductModel> Get()
    {
        return database.Products.Where(x => x.IsDeleted != true)
         .Select(x => mapper.Map<ProductModel>(x));
    }
    public IEnumerable<ProductModel> Get(ProductQueryModel query)
    {
        return database.Products.Where(x => x.IsDeleted != true &&
                                         (x.Name.Contains(query.Keywords ?? string.Empty) ||
                                         x.SKU.Contains(query.Keywords ?? string.Empty) ||
                                         x.Keywords.Contains(query.Keywords ?? string.Empty) ||
                                         x.Description.Contains(query.Keywords ?? string.Empty) ||
                                            x.Id.Contains(query.Keywords ?? string.Empty)
                                         ))
          .Select(x => mapper.Map<ProductModel>(x)).Skip((query.PageIndex - 1) * query.Limit).Take(query.Limit);

    }
    public Task<ProductModel> GetDetail(string Id)
    {
        var data = new ProductModel();

        var hexagram = database.Products.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        ;
        if (hexagram != null)
        {
            mapper.Map(hexagram, data);
        }

        return Task.FromResult(data);
    }
}
