namespace VIVU.Shared.Model;

public class SalesOrderModel
{
    public string Id { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Thông tin khách hàng
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;

    /// <summary>
    /// Ship to và bill to
    /// </summary>
    public string ShipToAddress { get; set; } = string.Empty;
    public string BillToAddress { get; set; } = string.Empty;

    /// <summary>
    /// Thời gian
    /// </summary>
    public DateTime ShipDate { get; set; }
    public decimal ShipFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Status { get; set; } 
    public List<SalesOrderDetailModel>? OrderDetail { get; set; }
}
public class SalesOrderDetailModel
{
    public string ProductId { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}
