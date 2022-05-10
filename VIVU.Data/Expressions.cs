namespace VIVU.Data;
public static class Expressions
{
    public static IServiceCollection RegisterDatabaseCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"), sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(10);
                sqlServerOptions.CommandTimeout(60000);
            });
        });
        

        return services;
    }
}

