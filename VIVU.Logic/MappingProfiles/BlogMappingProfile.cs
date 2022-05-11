namespace VIVU.Logic.MappingProfiles;

public class BlogMappingProfile : Profile
{
    public BlogMappingProfile()
    {
        CreateMap<CreateBlogCommand, Blog>();
        CreateMap<UpdateBlogCommand, Blog>();
    }
}
