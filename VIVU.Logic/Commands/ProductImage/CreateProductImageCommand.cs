
namespace VIVU.Logic.Commands;

public class CreateProductImageCommand : ProductImageModel, IAuditCommand, IRequest<CommonCommandResultHasData<ProductImageModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
