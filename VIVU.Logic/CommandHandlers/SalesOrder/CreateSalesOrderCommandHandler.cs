namespace VIVU.Logic.CommandHandlers;

public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, CommonCommandResultHasData<SalesOrderModel>>
{
    private readonly AppDatabase database;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ErrorConfig errorConfig;

    public CreateSalesOrderCommandHandler(AppDatabase database,
        IMediator mediator,
        IMapper mapper,
        IOptions<ErrorConfig> errorConfig)
    {
        this.database = database;
        this.mediator = mediator;
        this.mapper = mapper;
        this.errorConfig = errorConfig.Value;
    }

    public Task<CommonCommandResultHasData<SalesOrderModel>> Handle(
        CreateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var result = new CommonCommandResultHasData<SalesOrderModel>();

        try
        {
            var orderId = Nanoid.Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 12);
            request.Id = orderId;
            var order = mapper.Map<SalesOrder>(request);
            order.SetCreatedAudit(request.UserName);

            database.SalesOrders.Add(order);

            var orderDetail = mapper.Map<List<SalesOrderDetail>>(request.OrderDetail);
            orderDetail.ForEach(x => x.SalesOrderId = orderId);
            database.SalesOrderDetails.AddRange(orderDetail);
            database.SaveChanges();

            result.Success = true;
            result.Data = mapper.Map<SalesOrderModel>(request);
        }
        catch (DbUpdateException ex)
        {
            if (database.Products.SingleOrDefault(x => x.Id == request.Id) != null)
            {
                result.Message = errorConfig.GetByKey("DuplicateProduct");
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
