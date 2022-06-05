namespace VIVU.Logic.CommandHandlers;

public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public UpdateSalesOrderCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var salesOrder = database.SalesOrders.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (salesOrder != null)
            {
                mapper.Map(request, salesOrder);
                salesOrder.SetUpdatedAudit(request.UserName);
                database.Update(salesOrder);

                var orderDetail = database.SalesOrderDetails.Where(x => x.SalesOrderId == request.Id && !x.IsDeleted).ToList();
                database.UpdateRange(orderDetail);
                database.SaveChanges();
                result.Success = true;
            }
            else
            {
                result.Message = errorConfig.GetByKey("NotFoundProduct");
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }

        return Task.FromResult(result);
    }
}