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
        throw new NotImplementedException();
    }

    public IEnumerable<ProductModel> Get(string? keywords)
    {
        return database.Products.Where(x => x.IsDeleted != true &&
                                          (x.Name.Contains(keywords ?? string.Empty) ||
                                          x.SKU.Contains(keywords ?? string.Empty) ||
                                          x.Keywords.Contains(keywords ?? string.Empty) ||
                                          x.Description.Contains(keywords ?? string.Empty)))
           .Select(x => mapper.Map<ProductModel>(x));
    }

    public Task<ProductModel> GetDetail(string Id)
    {
        var data = new ProductModel();

        var hexagram = database.Products.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        if (hexagram != null)
        {
            mapper.Map(hexagram, data);
        }

        return Task.FromResult(data);
    }
}
