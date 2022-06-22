namespace VIVU.Common.Models;
public class CommonAudit
{
    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public CommonAudit SetCreatedAudit(string userName)
    {
        this.CreatedAt = DateTime.Now;
        this.CreatedBy = userName;
        this.UpdatedAt = DateTime.Now;
        this.UpdatedBy = userName;
        return this;
    }

    public CommonAudit SetUpdatedAudit(string userName)
    {
        this.UpdatedBy = userName;
        this.UpdatedAt = DateTime.Now;
        return this;
    }

    public CommonAudit MarkAsDeleted(string userName)
    {
        this.IsDeleted = true;
        this.SetUpdatedAudit(userName);
        return this;
    }
}

