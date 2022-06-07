namespace VIVU.Shared.Model;

public class ProductImageModel
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string UrlLink { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
}
