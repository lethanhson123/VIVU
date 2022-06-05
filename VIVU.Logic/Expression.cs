using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Logic.Queries.Implements;

namespace VIVU.Logic
{
    public static class Expression
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IBlogQueries, BlogQueries>();
            services.AddScoped<IProductQueries, ProductQueries>();
            services.AddScoped<IMarketLeadQueries, MarketLeadQueries>();
            services.AddScoped<ICustomerQueries, CustomerQueries>();
            services.AddScoped<IProductImageQueries, ProductImageQueries>();
            return services;
        }
    }
}
