using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Shared.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string TaxCode { get; set; } = string.Empty;
        public bool? IsVerifiedEmail { get; set; }
        public bool? IsVerifiedPhone { get; set; }
        public bool? IsAcceptsMarketing { get; set; }
        public DateTime? RegisterAt { get; set; }

        /// <summary>
        /// Dữ liệu khách hàng cá nhân
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;
        public DateTime? IdentityAt { get; set; }
        public string IdentityPlace { get; set; } = string.Empty;
        public string IdentityType { get; set; } = string.Empty;

        /// <summary>
        /// Dữ liệu khách hàng doanh nghiệp
        /// </summary>
        public string ContactFullName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        /// <summary>
        /// Channel: facebook, link, website,...
        /// </summary>
        public string Channel { get; set; } = string.Empty;
    }
}
