namespace VIVU.Data.Entities;
public class Blog : CommonAudit
{
    public int? WebsiteId { get; set; } = 0;
    public string WebsiteDomain { get; set; } = string.Empty;
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
    public Blog Initialization()
    {       
        if (string.IsNullOrEmpty(DatePostDisplay))
        {
            DatePostDisplay = DatePost.ToString("dd/MM/yyyy HH:mm:ss");
        }
        if (string.IsNullOrEmpty(URLExtension))
        {
            URLExtension = AppGlobal.URLExtension;
        }
        if (string.IsNullOrEmpty(URLExtension))
        {
            URLExtension = AppGlobal.URLExtension;
        }
        if (string.IsNullOrEmpty(URL))
        {
            URL = AppGlobal.ConvertNameToCode(Name);
        }
        if (string.IsNullOrEmpty(DescriptionOriginal))
        {
            DescriptionOriginal = AppGlobal.RemoveHTMLTags(DescriptionHTML);
        }
        if (string.IsNullOrEmpty(ContentOriginal))
        {
            ContentOriginal = AppGlobal.RemoveHTMLTags(ContentHTML);
        }
        if (string.IsNullOrEmpty(Display))
        {
            Display = Name;
        }
        if (string.IsNullOrEmpty(METAKeyword))
        {
            METAKeyword = Name;
        }
        if (string.IsNullOrEmpty(METAKeywordNews))
        {
            METAKeywordNews = Name;
        }
        if (string.IsNullOrEmpty(METADescription))
        {
            METADescription = DescriptionOriginal;
        }
        if (string.IsNullOrEmpty(URLFull))
        {
            URLFull = WebsiteDomain + URLCode + "/" + URL + "-" + Id + URLExtension;
        }
        if (string.IsNullOrEmpty(URLImage))
        {
            URLImage = WebsiteDomain + "/" + AppGlobal.ImagesDirectory + "/" + ImageFileName;
        }
        if (string.IsNullOrEmpty(URLShareFacebook))
        {
            URLShareFacebook = "https://www.facebook.com/sharer/sharer.php?u=" + URLFull;
        }
        if (string.IsNullOrEmpty(URLShareTwitter))
        {
            URLShareTwitter = "https://twitter.com/intent/tweet?text=" + Name + "&url=" + URLFull;
        }
        if (string.IsNullOrEmpty(URLSharePinterest))
        {
            URLSharePinterest = "https://www.pinterest.com/pin-builder/?url=" + URLFull + "&media=" + URLImage + "&description=" + Name + "&method=button";
        }
        if (string.IsNullOrEmpty(URLShareLinkedin))
        {
            URLShareLinkedin = "https://www.linkedin.com/sharing/share-offsite/?url=" + URLFull;
        }
        return this;
    }
}

