using System.ComponentModel.DataAnnotations;

namespace VIVU.Ultils;

public enum SaleOrderEnum
{
    [Display(Name = "Tạo mới")]
    NEW = 0,
    [Display(Name = "Xác nhận")]
    CONFIRM = 1,
    [Display(Name = "Đang giao")]
    SHIPPING = 2,
    [Display(Name = "Hoàn tất")]
    COMPLETED = 3,
    [Display(Name = "Đã hủy")]
    CANCEL = 4,
    [Display(Name = "Lỗi phát sinh")]
    ERROR = 5,
    [Display(Name = "Không nhận hàng")]
    NORECEIVED = 6,
    [Display(Name = "Hết hàng")]
    OUTSTOCK = 7
}
