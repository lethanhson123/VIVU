﻿namespace VIVU.Logic.CommandHandlers;

public class UpdateMarketLeadCommandHandler : IRequestHandler<UpdateMarketLeadCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateMarketLeadCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateMarketLeadCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var marketLead = database.MarketLeads.FirstOrDefault(x => x.Id == request.Id);

            if (marketLead != null)
            {
                mapper.Map(request, marketLead);
                marketLead.SetUpdatedAudit(request.UserName);

                database.Update(marketLead);
                database.SaveChanges();

                result.Success = true;
            }
            else
            {
                result.Message = errorConfig.GetByKey("NotFoundMarketLead");
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}

