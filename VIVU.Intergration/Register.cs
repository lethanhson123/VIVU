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
            return services;
        }

        public static IServiceCollection RegisterClientHelper(this IServiceCollection services)
        {
           // services.AddScoped<IAuthenticateHelper, AuthenticateHelper>();
            return services;
        }
    }
}
