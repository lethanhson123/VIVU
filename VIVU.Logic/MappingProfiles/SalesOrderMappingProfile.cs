
namespace VIVU.Logic.MappingProfiles;

public class SalesOrderMappingProfile : Profile
{
    public SalesOrderMappingProfile()
    {
        CreateMap<CreateSalesOrderCommand, SalesOrder>();
        CreateMap<Product, SalesOrderDetail>()
                    .ForMember(x => x.ProductId, y => y.MapFrom(src => src.Id))
                    .ForMember(x => x.ProductName, y => y.MapFrom(src => src.Name))
                    .ForMember(x => x.TotalPrice, y => y.Ignore())
                    .ForMember(x => x.Id, y => y.Ignore());

    }
}
