
namespace VIVU.Logic.CommandHandlers
{
    public class CreateMarketLeadCommandHandler : IRequestHandler<CreateMarketLeadCommand, CommonCommandResultHasData<MarketLeadModel>>
    {
        private readonly AppDatabase database;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public CreateMarketLeadCommandHandler(AppDatabase database,
            IMediator mediator,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mediator = mediator;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResultHasData<MarketLeadModel>> Handle(
            CreateMarketLeadCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResultHasData<MarketLeadModel>();

            try
            {
                var tag = mapper.Map<MarketLead>(request);
                tag.SetCreatedAudit(request.UserName);

                database.MarketLeads.Add(tag);
                database.SaveChanges();

                result.Success = true;
                result.Data = mapper.Map<MarketLeadModel>(tag);
            }
            catch (DbUpdateException ex)
            {
                if (database.MarketLeads.SingleOrDefault(x => x.Id == request.Id) != null)
                {
                    result.Message = errorConfig.GetByKey("DuplicateMarketLead");
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
