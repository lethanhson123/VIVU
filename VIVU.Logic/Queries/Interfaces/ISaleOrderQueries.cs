namespace VIVU.Logic.Queries.Interfaces;

public interface ISaleOrderQueries
{
    IEnumerable<SalesOrderModelResponse> Get();
    IEnumerable<SalesOrderModelResponse> Get(SalesOrderQueryModel query);
    Task<SalesOrderModelResponse> GetDetail(string Id);
}
