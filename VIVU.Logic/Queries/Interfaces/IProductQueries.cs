namespace VIVU.Logic.Queries.Interfaces;

public interface IProductQueries
{
    IEnumerable<ProductModel> Get();
    IEnumerable<ProductModel> Get(ProductQueryModel query);
    IEnumerable<ProductModel> Get(string? keywords);
    Task<ProductModel> GetDetail(int Id);
}
