namespace VIVU.Logic.MappingProfiles;

public class ProductImageMappingProfile : Profile
{
    public ProductImageMappingProfile()
    {
        CreateMap<ProductImageModel, ProductImage>();
        CreateMap<ProductImage, ProductImageModel>();
        CreateMap<CreateProductImageCommand, ProductImage>();
        CreateMap<UpdateProductImageCommand, ProductImage>();
        CreateMap<DeleteProductImageCommand, ProductImage>();
    }
}
