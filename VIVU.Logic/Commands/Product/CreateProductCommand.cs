
namespace VIVU.Logic.Commands;

public class CreateProductCommand : ProductModel, IAuditCommand, IRequest<CommonCommandResultHasData<ProductModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
