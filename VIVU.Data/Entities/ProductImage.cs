namespace VIVU.Data.Entities;

public class ProductImage : CommonAudit
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string UrlLink { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
