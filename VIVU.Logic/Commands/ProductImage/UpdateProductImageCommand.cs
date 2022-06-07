namespace VIVU.Logic.Commands;

public class UpdateProductImageCommand : ProductImageModel, IAuditCommand, IRequest<CommonCommandResultHasData<ProductImageModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
