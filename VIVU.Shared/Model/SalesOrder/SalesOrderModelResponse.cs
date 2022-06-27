namespace VIVU.Shared.Model;

public class SalesOrderModelResponse
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
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }

    public decimal AmountWithoutTax { get; set; }
    public decimal ShipFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string ChanelId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public List<SalesOrderDetailModelResponse>? OrderDetail { get; set; }
}
public class SalesOrderDetailModelResponse
{
    public int Id { get; set; }
    public string ProductName { get; set; } = String.Empty;
    public string ProductId { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; } // Giá bán
    public decimal DeliverPrice { get; set; } // Giá bán cuối cùng
    public decimal TotalPrice { get; set; }
}
