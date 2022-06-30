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
        public string SaleOrderGetWithQuery { get; set; } = string.Empty;
        public string SaleOrder { get; set; } = string.Empty;
        public string SaleOrderStatus { get; set; } = string.Empty;
        public string ProductCategoryGetWithQuery { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;

        /// <summary>
        /// Tag url
        /// </summary>
        public string CreateTag { get; set; } = string.Empty;
        public string UpdateTag { get; set; } = string.Empty;
        public string DeleteTag { get; set; } = string.Empty;
        public string GetTag { get; set; } = string.Empty;
        public string GetTagWithQuery { get; set; } = string.Empty;

        /// <summary>
        /// Category url
        /// </summary>
        public string CreateCategory { get; set; } = string.Empty;
        public string UpdateCategory { get; set; } = string.Empty;
        public string DeleteCategory { get; set; } = string.Empty;
        public string GetCategory { get; set; } = string.Empty;
        public string GetCategoryWithQuery { get; set; } = string.Empty;

        /// <summary>
        /// Blog url
        /// </summary>
        public string CreateBlog { get; set; } = string.Empty;
        public string UpdateBlog { get; set; } = string.Empty;
        public string DeleteBlog { get; set; } = string.Empty;
        public string GetBlog { get; set; } = string.Empty;
        public string GetBlogWithQuery { get; set; } = string.Empty;
    }
}
