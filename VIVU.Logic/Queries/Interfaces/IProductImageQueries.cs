namespace VIVU.Logic.Queries.Interfaces;

public interface IProductImageQueries
{
    IEnumerable<ProductImageModel> Get();
    IEnumerable<ProductImageModel> Get(ProductImageQueryModel query);
    IEnumerable<ProductImageModel> Get(string? keywords);
    Task<ProductImageModel> GetDetail(int Id);
}
