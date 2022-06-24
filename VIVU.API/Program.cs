

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    foreach (var desc in builder.Services.BuildServiceProvider()
        .GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
    {
        options.SwaggerDoc(desc.GroupName, new OpenApiInfo { Title = desc.GroupName, Version = desc.ApiVersion.ToString() });
    }
});
builder.Services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<AppDatabase>()
              .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
});
builder.Services.AddCustomApiVersioning();
builder.Services.AddSwagerUI(builder.Configuration);
builder.Services.AddAutoMapper(typeof(BlogMappingProfile));
builder.Services.AddAuthenticationJwt(builder.Configuration);
builder.Services.AddMediatR(typeof(CreateBlogCommand).Assembly);
builder.Services.AddQueries();
builder.Services.Configure<AuthenticateConfig>(builder.Configuration.GetSection(AuthenticateConfig.ConfigName));
builder.Services.Configure<ErrorConfig>(builder.Configuration.GetSection(ErrorConfig.ConfigName));


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddSqlServerDatabase<AppDatabase>(
        builder.Configuration.GetConnectionString("Database"));

var app = builder.Build();

/// <summary>
/// Migrate database
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var applicationDb = scope.ServiceProvider.GetRequiredService<AppDatabase>();



    await applicationDb.Database.MigrateAsync();

    if (app.Environment.IsDevelopment())
    {
        //await ApplicationDatabaseInitializer.InitializeAsync(applicationDb);        
    }
}

app.UseCors("CORS");
app.UseSwagger();
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
            options.DefaultModelsExpandDepth(-1);
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        }
    });
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

