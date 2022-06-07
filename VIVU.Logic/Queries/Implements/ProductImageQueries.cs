namespace VIVU.Logic.Queries.Implements;

public class ProductImageQueries : IProductImageQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public ProductImageQueries(AppDatabase database,
        IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<ProductImageModel> Get()
    {
        return database.ProductImages.Where(x => x.IsDeleted != true)
         .Select(x => mapper.Map<ProductImageModel>(x));
    }
    public IEnumerable<ProductImageModel> Get(ProductImageQueryModel query)
    {
        return database.ProductImages.Where(x => x.IsDeleted != true &&
                                                  x.ProductId == query.ProductId )
           .Select(x => mapper.Map<ProductImageModel>(x));
    }

    public IEnumerable<ProductImageModel> Get(string? keywords)
    {
        return database.ProductImages.Where(x => x.IsDeleted != true &&
                                          (x.Title.Contains(keywords ?? string.Empty)))
           .Select(x => mapper.Map<ProductImageModel>(x));
    }

    public Task<ProductImageModel> GetDetail(int Id)
    {
        var data = new ProductImageModel();

        var hexagram = database.ProductImages.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        if (hexagram != null)
        {
            mapper.Map(hexagram, data);
        }

        return Task.FromResult(data);
    }
}
