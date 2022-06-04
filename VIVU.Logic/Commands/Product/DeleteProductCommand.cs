namespace VIVU.Logic.Commands;

public class DeleteProductCommand : IAuditCommand, IRequest<CommonCommandResult>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
    public int Id { get; set; }
}

