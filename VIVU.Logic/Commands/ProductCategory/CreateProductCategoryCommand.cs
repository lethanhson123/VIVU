
namespace VIVU.Logic.Commands;

public class CreateProductCategoryCommand : ProductCategoryModel, IAuditCommand, IRequest<CommonCommandResultHasData<ProductCategoryModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
