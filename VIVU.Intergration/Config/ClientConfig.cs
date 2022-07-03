using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration.Config
{
    public class ClientConfig
    {
        public static string ConfigName { get; set; } = "Client";
        public string Host { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
       
        /// <summary>
        /// Product
        /// </summary>
        public string ProductGetWithQuery { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string ProductImageGetWithQuery { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string SaleOrderGetWithQuery { get; set; } = string.Empty;
        public string SaleOrder { get; set; } = string.Empty;
        public string SaleOrderStatus { get; set; } = string.Empty;
        public string ProductCategoryGetWithQuery { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;

        /// <summary>
        /// Tag url
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        public string TagGetWithQuery { get; set; } = string.Empty;

        /// <summary>
        /// Category url
        /// </summary>
        public string Categories { get; set; } = string.Empty;
        public string CategoriesGetWithQuery { get; set; } = string.Empty;

        /// <summary>
        /// Blog url
        /// </summary>
        public string Blog { get; set; } = string.Empty;
        public string BlogGetWithQuery { get; set; } = string.Empty;
    }
}
