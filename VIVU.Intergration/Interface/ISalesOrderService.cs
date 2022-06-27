namespace VIVU.Intergration.Interface;

public interface ISalesOrderService
{
    Task<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>> Get(string token = "");
    Task<CommonResponseModel<IEnumerable<SalesOrderDetailModelResponse>>> Get(SalesOrderQueryModel query, string token = "");
    Task<CommonResponseModel<SalesOrderDetailModelResponse>> GetDetail(string Id, string token = "");
    Task<CommonResponseModel<SalesOrderModel>> Create(SalesOrderModel request, string token = "");
    Task<CommonResponseModel<object>> Update(SalesOrderModel request, string token = "");
    Task<CommonResponseModel<object>> Delete(string Id, string token = "");
}
