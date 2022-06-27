namespace VIVU.Logic.Commands;

public class UpdateProductCategoryCommand : ProductCategoryModel, IAuditCommand, IRequest<CommonCommandResult>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
