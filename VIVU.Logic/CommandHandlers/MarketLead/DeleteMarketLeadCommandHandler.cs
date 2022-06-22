namespace VIVU.Logic.CommandHandlers
{
    public class DeleteMarketLeadCommandHandler :
        IRequestHandler<DeleteMarketLeadCommand, CommonCommandResult>
    {
        private readonly AppDatabase database;
        private readonly IMapper mapper;
        private readonly ErrorConfig errorConfig;

        public DeleteMarketLeadCommandHandler(AppDatabase database,
            IMapper mapper,
            IOptions<ErrorConfig> errorConfig)
        {
            this.database = database;
            this.mapper = mapper;
            this.errorConfig = errorConfig.Value;
        }

        public Task<CommonCommandResult> Handle(DeleteMarketLeadCommand request, CancellationToken cancellationToken)
        {
            var result = new CommonCommandResult();

            try
            {
                var marketLead = database.MarketLeads.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted );

                if (marketLead != null)
                {
                    mapper.Map(request, marketLead);
                    marketLead.MarkAsDeleted(request.UserName);

                    database.MarketLeads.Update(marketLead);
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
