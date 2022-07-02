namespace VIVU.Logic.MappingProfiles;

public class BlogMappingProfile : Profile
{
    public BlogMappingProfile()
    {
        CreateMap<CreateBlogCommand, Blog>();
        CreateMap<UpdateBlogCommand, Blog>()
            .ForMember(x=>x.Id ,y=>y.Ignore())
            .ForMember(x=>x.CreatedBy ,y=>y.Ignore())
            .ForMember(x=>x.CreatedAt ,y=>y.Ignore());
        CreateMap<Blog, BlogModel>();
        CreateMap<Blog, BlogDetailModel>();
    }
}
