namespace VIVU.Logic.Queries.Interfaces;

public interface IProductCategoryQueries
{
    IEnumerable<ProductCategoryModel> Get();
    IEnumerable<ProductCategoryModel> Get(ProductCategoryQueryModel query);
    Task<ProductCategoryModel> GetDetail(string Id);
}
