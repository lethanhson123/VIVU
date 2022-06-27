namespace VIVU.Logic.Queries.Implements;

public class ProductCategoryQueries : IProductCategoryQueries
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;

    public ProductCategoryQueries(AppDatabase database,
        IMapper mapper)
    {
        this.database = database;
        this.mapper = mapper;
    }
    public IEnumerable<ProductCategoryModel> Get()
    {
        return database.Products.Where(x => x.IsDeleted != true)
         .Select(x => mapper.Map<ProductCategoryModel>(x));
    }
    public IEnumerable<ProductCategoryModel> Get(ProductCategoryQueryModel query)
    {
        return database.ProductCategories.Where(x => !x.IsDeleted &&
                                         (x.Name.Contains(query.Keywords ?? string.Empty) ||                                     
                                         x.Description.Contains(query.Keywords ?? string.Empty) ||
                                         x.Id.Contains(query.Keywords ?? string.Empty)
                                         ))
          .Select(x => mapper.Map<ProductCategoryModel>(x)).Skip((query.PageIndex - 1) * query.Limit).Take(query.Limit);

    }
    public Task<ProductCategoryModel> GetDetail(string Id)
    {
        var data = new ProductCategoryModel();

        var hexagram = database.ProductCategories.FirstOrDefault(x => x.Id == Id &&
                                                          x.IsDeleted != true);
        ;
        if (hexagram != null)
        {
            mapper.Map(hexagram, data);
        }

        return Task.FromResult(data);
    }
}
