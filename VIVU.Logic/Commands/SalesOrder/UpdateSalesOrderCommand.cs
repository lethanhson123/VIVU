namespace VIVU.Logic.Commands;

public class UpdateSalesOrderCommand : SalesOrderModel, IAuditCommand, IRequest<CommonCommandResultHasData<object>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
