namespace VIVU.Common.Models;
public class CommonAudit
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

    public CommonAudit SetCreateAudit(string userName)
    {
        CreatedBy = userName;
        CreatedAt = DateTime.Now;
        UpdatedBy = userName;
        UpdatedAt = DateTime.Now;
        RowVersion = 1;
        return this;
    }
    public CommonAudit SetUpdateAudit(string userName)
    {
        UpdatedBy = userName;
        UpdatedAt = DateTime.Now;
        RowVersion = RowVersion + 1;
        return this;
    }
}

