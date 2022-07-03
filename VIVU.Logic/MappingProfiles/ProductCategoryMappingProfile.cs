namespace VIVU.Logic.MappingProfiles;

public class ProductCategoryMappingProfile : Profile
{
    public ProductCategoryMappingProfile()
    {
        CreateMap<ProductCategoryModel, ProductCategory>();
        CreateMap<ProductCategory, ProductCategoryModel>();
        CreateMap<CreateProductCategoryCommand, ProductCategory>();
        CreateMap<UpdateProductCategoryCommand, ProductCategory>();
        CreateMap<DeleteProductCategoryCommand, ProductCategory>();
    }
}
