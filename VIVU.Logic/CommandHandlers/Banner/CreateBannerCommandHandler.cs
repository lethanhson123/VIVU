using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Configs;
using VIVU.Shared.Model;

namespace VIVU.Logic.CommandHandlers
{
    public class CreateBannerCommandHandler
         : IRequestHandler<CreateBannerCommand, CommonCommandResultHasData<BannerModel>>
    {
        private readonly AppDatabase database;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public CreateBannerCommandHandler(AppDatabase database,
            IMediator mediator,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mediator = mediator;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResultHasData<BannerModel>> Handle(
            CreateBannerCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResultHasData<BannerModel>();

            try
            {
                var tag = mapper.Map<Banner>(request);
                tag.SetCreatedAudit(request.UserName);

                database.Banners.Add(tag);
                database.SaveChanges();

                result.Success = true;
                result.Data = mapper.Map<BannerModel>(tag);
            }
            catch (DbUpdateException ex)
            {
                if (database.Banners.SingleOrDefault(x => x.Id == request.Id) != null)
                {
                    result.Message = errorConfig.GetByKey("DuplicateBanner");
                }
                else
                {
                    result.Message = ex.Message;
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
