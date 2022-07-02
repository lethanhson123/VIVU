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
            //  var order = mapper.Map<SalesOrder>(request);
            var order = database.SalesOrders.Where(x => x.Id == request.Id).FirstOrDefault();
            if (order == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrder");
                return Task.FromResult(result);
            }
            //set data mới
            order.ShipToAddress = request.ShipToAddress;
            order.BillToAddress = request.BillToAddress;
            order.ShipDate = request.ShipDate;
            order.CustomerPhone = request.CustomerPhone;
            order.CustomerName = request.CustomerName;
            order.CustomerEmail = request.CustomerEmail;
            order.CustomerAddress = request.CustomerAddress;
            order.CustomerId = request.CustomerId;

            order.SetUpdatedAudit(request.UserName);
            if (request.OrderDetail == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrderDatail");
                return Task.FromResult(result);

            }
            var orderDetailOld = database.SalesOrderDetails.Where(x => x.ProductId == request.Id).ToList();
            orderDetailOld.ForEach(x => x.MarkAsDeleted(request.UserName));
            var listProductId = request?.OrderDetail?.Select(x => x.ProductId).ToList();
            var product = database.Products.Where(x => listProductId!.Contains(x.Id)).ToList();

            var orderDetail = mapper.Map<List<SalesOrderDetail>>(product);
            decimal totalPrice = 0;
            if (orderDetail == null)
            {
                result.Message = errorConfig.GetByKey("NotExistsDataOrderDatail");
                return Task.FromResult(result);

            }
            foreach (var item in orderDetail)
            {
                foreach (var detail in request!.OrderDetail)
                {
                    if (item.ProductId == detail.ProductId)
                    {
                        item.Quantity = detail.Quantity;
                        item.TotalPrice = item.DeliverPrice * detail.Quantity;
                        item.SalesOrderId = request.Id;
                        totalPrice += item.DeliverPrice * detail.Quantity;
                    }
                }
            }
            order.TotalAmount = totalPrice - order.DiscountAmount + order.ShipFee;
            database.SalesOrders.Add(order);
            database.SalesOrderDetails.AddRange(orderDetail);
            database.SalesOrderDetails.UpdateRange(orderDetailOld);
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