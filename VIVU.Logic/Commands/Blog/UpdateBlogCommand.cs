
namespace VIVU.Logic.Commands;

public class UpdateBlogCommand : CommonAuditCommand, IRequest<CommonCommandResult>
{
    public int Id { get; set; } = 0;
    public int? ParentId { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;    
    public string Code { get; set; } = string.Empty;
    public int? SortOrder { get; set; }
    public DateTime DatePost { get; set; } = DateTime.Now;   
    public string Name { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
    public string DescriptionHTML { get; set; } = string.Empty;
    public string ContentHTML { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string METAKeyword { get; set; } = string.Empty;
    public string METAKeywordNews { get; set; } = string.Empty;
    public string METADescription { get; set; } = string.Empty;
    public string URLCode { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public string ImageFileName { get; set; } = string.Empty;
    public bool IsBanner { get; set; } = false;
}
