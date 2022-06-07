namespace VIVU.Logic.Queries.Interfaces;

public interface IMarketLeadQueries
{
    IEnumerable<MarketLeadModel> Get();
    IEnumerable<MarketLeadModel> Get(MarketLeadQueryModel query);
    IEnumerable<MarketLeadModel> Get(string? keywords);
    Task<MarketLeadModel> GetDetail(int Id);
}
