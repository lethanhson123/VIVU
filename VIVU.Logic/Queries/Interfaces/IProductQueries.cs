namespace VIVU.Logic.Queries.Interfaces;

public interface IProductQueries
{
    IEnumerable<ProductModel> Get();
    IEnumerable<ProductModel> Get(ProductQueryModel query);
    Task<ProductModel> GetDetail(string Id);
}
