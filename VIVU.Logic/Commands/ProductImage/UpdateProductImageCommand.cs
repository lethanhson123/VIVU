namespace VIVU.Logic.Commands;

public class UpdateProductImageCommand : ProductImageModel, IAuditCommand, IRequest<CommonCommandResult>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
