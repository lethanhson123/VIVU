﻿
namespace VIVU.Logic.Commands;

public class UpdateBlogCommand : CommonAuditCommand, IRequest<CommonCommandResultHasData<Blog>>
{
    public int Id { get; set; } = 0;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public int? ParentId { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int? RowVersion { get; set; }
    public string Code { get; set; } = string.Empty;
    public int? SortOrder { get; set; }
    public DateTime DatePost { get; set; } = DateTime.Now;
    public string DatePostDisplay { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
    public string DescriptionHTML { get; set; } = string.Empty;
    public string DescriptionOriginal { get; set; } = string.Empty;
    /// <summary>
    /// DescriptionOriginal = remove all HTML tags of DescriptionHTML
    /// </summary>
    public string ContentHTML { get; set; } = string.Empty;
    public string ContentOriginal { get; set; } = string.Empty;
    /// <summary>
    /// ContentOriginal = remove all HTML tags of ContentHTML
    /// </summary>
    public string Author { get; set; } = string.Empty;
    public string METAKeyword { get; set; } = string.Empty;
    public string METAKeywordNews { get; set; } = string.Empty;
    public string METADescription { get; set; } = string.Empty;
    public string URLCode { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public string URLExtension { get; set; } = string.Empty;
    public string URLFull { get; set; } = string.Empty;
    /// <summary>
    /// URLFull = Domain + URLCode + "/" + URL + "-" + Id + URLExtension;
    /// </summary>
    public string ImageFileName { get; set; } = string.Empty;
    public string URLImage { get; set; } = string.Empty;
    /// <summary>
    /// URLImage = Domain + "/Images/" + ImageFileName;
    /// </summary>
    public string URLShareFacebook { get; set; } = string.Empty;
    /// <summary>
    /// URLShareFacebook = "https://www.facebook.com/sharer/sharer.php?u=" + URLFull;
    /// </summary>
    public string URLShareTwitter { get; set; } = string.Empty;
    /// <summary>
    /// URLShareTwitter = "https://twitter.com/intent/tweet?text=" + Name + "&url=" + URLFull;
    /// </summary>    
    public string URLSharePinterest { get; set; } = string.Empty;
    /// <summary>
    /// URLSharePinterest = "https://www.pinterest.com/pin-builder/?url=" + URLFull + "&media=" + URLImage + "&description=" + Name + "&method=button";
    /// </summary>
    public string URLShareLinkedin { get; set; } = string.Empty;
    /// <summary>
    /// URLShareLinkedin = "https://www.linkedin.com/sharing/share-offsite/?url=" + URLFull;
    /// </summary>
    public bool IsBanner { get; set; } = false;
}
