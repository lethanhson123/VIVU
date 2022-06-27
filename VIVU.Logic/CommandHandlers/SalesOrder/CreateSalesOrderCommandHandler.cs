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
            order.Id = orderId;
            order.SetCreatedAudit(request.UserName);
            if (request.OrderDetail == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrderDatail");
                return Task.FromResult(result);

            }

            var listProductId = request?.OrderDetail?.Select(x => x.ProductId).ToList();
            var product = database.Products.Where(x => listProductId!.Contains(x.Id)).ToList();

            var orderDetail = mapper.Map<List<SalesOrderDetail>>(product);
            decimal totalPrice = 0;
            if(orderDetail == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrderDatail");
                return Task.FromResult(result);

            }
            foreach (var item in orderDetail)
            {
                foreach (var detail in request!.OrderDetail)
                {
                    if(item.ProductId == detail.ProductId)
                    {
                        item.Quantity = detail.Quantity;
                        item.TotalPrice = item.DeliverPrice * detail.Quantity;
                        item.SalesOrderId = orderId;
                        totalPrice += item.DeliverPrice * detail.Quantity;
                    }
                }
            }
            order.TotalAmount = totalPrice - order.DiscountAmount + order.ShipFee;
            database.SalesOrders.Add(order);
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
