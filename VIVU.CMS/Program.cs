var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.RegisterClientService();
builder.Services.RegisterClientHelper();
builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
    o.Providers.Add<BrotliCompressionProvider>();
    o.Providers.Add<GzipCompressionProvider>();
});
builder.Services.AddHttpClient();
builder.Services.RegisterClientService();
builder.Services.RegisterClientHelper();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/user/login";
    });

builder.Services.AddResponseCaching();
builder.Services.AddHttpClient();


/// <summary>
///  Add config
/// </summary>
builder.Services
    .Configure<AuthenticateConfig>(builder.Configuration.GetSection(AuthenticateConfig.ConfigName));
builder.Services
    .Configure<ErrorConfig>(builder.Configuration.GetSection(ErrorConfig.ConfigName));
builder.Services
    .Configure<SiteConfig>(builder.Configuration.GetSection(SiteConfig.ConfigName));
builder.Services
    .Configure<SitePageConfig>(builder.Configuration.GetSection(SitePageConfig.ConfigName));
builder.Services
    .Configure<ClientConfig>(builder.Configuration.GetSection(ClientConfig.ConfigName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
