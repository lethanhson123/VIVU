
namespace VIVU.Logic.Commands;

public class UpdateBlogCommand : CommonAuditCommand, IRequest<CommonCommandResult>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Meta { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public DateTime PostDate { get; set; }
    public bool IsPublished { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();
    public List<TagModel>? Tags { get; set; } = new List<TagModel>();
}
