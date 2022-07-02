using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Intergration
{
    public static class Register
    {
        public static IServiceCollection RegisterClientService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAuthenticateHelper, AuthenticateHelper>();
            services.AddScoped<ICategoriesService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            return services;
        }

        public static IServiceCollection RegisterClientHelper(this IServiceCollection services)
        {
           // services.AddScoped<IAuthenticateHelper, AuthenticateHelper>();
            return services;
        }
    }
}
