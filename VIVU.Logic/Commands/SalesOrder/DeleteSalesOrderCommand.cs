namespace VIVU.Logic.Commands;

public class DeleteSalesOrderCommand : IAuditCommand, IRequest<CommonCommandResult>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}

