namespace VIVU.Shared.Model;

public class SalesOrderModel
{
    public int Id { get; set; }
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
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }

    public decimal AmountWithoutTax { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public List<SalesOrderDetailModel>? OrderDetail { get; set; }
}
public class SalesOrderDetailModel
{
    public int Id { get; set; }
    public int SalesOrderId { get; set; }
    public string ProductSku { get; set; } = string.Empty;
}
