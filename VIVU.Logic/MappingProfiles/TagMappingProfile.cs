using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.MappingProfiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<TagModel, Tag>();
            CreateMap<TagModel, BlogTag>()
                .ForMember(x => x.TagId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.Id, y => y.Ignore());
            CreateMap<Tag, TagModel>();
            CreateMap<Tag, TagDetailModel>();
            CreateMap<CreateTagCommand, Tag>();
            CreateMap<UpdateTagCommand, Tag>();
        }
    }
}
