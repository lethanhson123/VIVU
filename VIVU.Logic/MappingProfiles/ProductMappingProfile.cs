namespace VIVU.Logic.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductModel, Product>();
        CreateMap<Product, ProductModel>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<DeleteProductCommand, Product>();
    }
}
