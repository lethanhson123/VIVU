namespace VIVU.Logic.Commands;

public class UpdateProductCommand : ProductModel, IAuditCommand, IRequest<CommonCommandResult>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
