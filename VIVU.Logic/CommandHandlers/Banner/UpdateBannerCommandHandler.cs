using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;

namespace VIVU.Logic.CommandHandlers
{
    public class UpdateBannerCommandHandler :
        IRequestHandler<UpdateBannerCommand, CommonCommandResult>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public UpdateBannerCommandHandler(AppDatabase database,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResult> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResult();

            try
            {
                var banner = database.Banners.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

                if (banner != null)
                {
                    mapper.Map(request, banner);
                    banner.SetUpdatedAudit(request.UserName);

                    database.Update(banner);
                    database.SaveChanges();

                    result.Success = true;
                }
                else
                {
                    result.Message = errorConfig.GetByKey("NotFoundBanner");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
