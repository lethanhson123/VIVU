namespace VIVU.Logic.Commands;

public class UpdateStatusSalesOrderCommand : IAuditCommand, IRequest<CommonCommandResultHasData<object>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public int Status { get; set; } 


}
