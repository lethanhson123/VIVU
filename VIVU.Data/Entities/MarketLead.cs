using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Data.Entities
{
    /// <summary>
    /// Quản lý thông tin khách hàng dạng tiềm năng
    /// </summary>
    public class MarketLead : CommonAudit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// Khách hàng biết đến qua kênh nào
        /// FB: Facebook
        /// WEB: Website
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// Mã tham chiếu kênh
        /// Khách hàng biết đến sản phẩm qua website với link https://zyx.xxx
        /// </summary>
        public string ReferenceId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
