namespace VIVU.Logic.Commands;

public class CreateSalesOrderCommand : SalesOrderModel, IAuditCommand, IRequest<CommonCommandResultHasData<SalesOrderModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
