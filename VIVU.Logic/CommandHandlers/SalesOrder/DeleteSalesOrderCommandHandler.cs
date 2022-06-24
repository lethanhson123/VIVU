namespace VIVU.Logic.CommandHandlers;

public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand, CommonCommandResult>
{
    private readonly AppDatabase database;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public DeleteSalesOrderCommandHandler(AppDatabase database,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResult> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResult();

        try
        {
            var order = database.SalesOrders.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

            if (order != null)
            {
                order.MarkAsDeleted(request.UserName);

                database.SalesOrders.Update(order);

                var orderDetail = database.SalesOrderDetails.Where(x => x.SalesOrderId == request.Id && !x.IsDeleted).ToList();
                orderDetail.ForEach(x => x.IsDeleted = true);
                database.SalesOrderDetails.UpdateRange(orderDetail);

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