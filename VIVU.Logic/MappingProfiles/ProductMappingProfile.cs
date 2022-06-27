namespace VIVU.Logic.MappingProfiles;

public class ProductCategoryMappingProfile : Profile
{
    public ProductCategoryMappingProfile()
    {
        CreateMap<ProductCategoryModel, Product>();
        CreateMap<Product, ProductCategoryModel>();
        CreateMap<CreateProductCategoryCommand, Product>();
        CreateMap<UpdateProductCategoryCommand, Product>();
        CreateMap<DeleteProductCategoryCommand, Product>();
    }
}
