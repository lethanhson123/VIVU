namespace VIVU.Logic.Commands;

public class CreateBannerCommand : BannerModel,
    IAuditCommand, IRequest<CommonCommandResultHasData<BannerModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
