namespace VIVU.Data.Entities;

public class ProductCategory : CommonAudit
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ParentId { get; set; } = string.Empty;
}
