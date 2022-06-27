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
        /// <summary>
        /// Product
        /// </summary>
        public string ProductGetWithQuery { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string SaleOrderGetWithQuery { get; set; } = string.Empty;
        public string SaleOrder { get; set; } = string.Empty;
    }
}
