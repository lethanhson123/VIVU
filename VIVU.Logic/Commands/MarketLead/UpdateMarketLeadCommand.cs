
namespace VIVU.Logic.Commands;

public class UpdateMarketLeadCommand : MarketLeadModel, IAuditCommand, IRequest<CommonCommandResultHasData<MarketLeadModel>>
{
    [JsonIgnore]
    public string UserName { get; set; } = string.Empty;
}
