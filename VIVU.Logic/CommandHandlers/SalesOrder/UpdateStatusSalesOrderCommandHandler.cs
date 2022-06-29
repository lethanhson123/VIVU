namespace VIVU.Logic.CommandHandlers;

public class UpdateStatusSalesOrderCommandHandler : IRequestHandler<UpdateStatusSalesOrderCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateStatusSalesOrderCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateStatusSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();
        try
        {
            //  var order = mapper.Map<SalesOrder>(request);
            var order = database.SalesOrders.Where(x => x.Id == request.Id).FirstOrDefault();
            if (order == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrder");
                return Task.FromResult(result);
            }
            order.Status = request.Status;
            database.SalesOrders.Update(order);
            database.SaveChanges();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}