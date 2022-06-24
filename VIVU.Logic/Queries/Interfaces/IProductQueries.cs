namespace VIVU.Logic.Queries.Interfaces;

public interface IProductQueries
{
    IEnumerable<ProductModel> Get();
    IEnumerable<ProductModel> Get(ProductQueryModel query);
    ProductModel GetDetail(string Id);
}
