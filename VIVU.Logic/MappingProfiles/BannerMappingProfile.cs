using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.MappingProfiles
{
    public class BannerMappingProfile : Profile
    {
        public BannerMappingProfile()
        {
            CreateMap<BannerModel, Banner>();
            CreateMap<Banner, BannerModel>();
            CreateMap<CreateBannerCommand, Banner>();
            CreateMap<UpdateBannerCommand, Banner>();
        }
    }
}
