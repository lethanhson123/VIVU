using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using VIVU.Logic.Configs;
using VIVU.Ultils.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
    o.Providers.Add<BrotliCompressionProvider>();
    o.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.AddResponseCaching();
builder.Services.AddHttpClient();

/// <summary>
/// Add singleton object
/// </summary>
builder.Services.AddSingleton<SiteMap>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        if (!string.IsNullOrEmpty(context.Context.Request.Query["v"]))
        {
            context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddHours(1).ToString("R") }); // Format RFC112
        }
    }
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(e =>
{
    e.MapControllerRoute(
       name: "fengshui",
       pattern: "tien-ich/phong-thuy.html",
       defaults: new { controller = "FengShui", action = "Index" });

    e.MapControllerRoute(
       name: "horoscope",
       pattern: "tien-ich/lap-la-so-tu-vi.html",
       defaults: new { controller = "Horoscope", action = "Index" });

    e.MapControllerRoute(
       name: "horoscope",
       pattern: "tien-ich/la-so-tu-vi.html",
       defaults: new { controller = "Horoscope", action = "Detail" });

    e.MapControllerRoute(
       name: "fengshui-phone-number",
       pattern: "tien-ich/phong-thuy-sim.html",
       defaults: new { controller = "FengShui", action = "PhoneNumber" });

    e.MapControllerRoute(
       name: "post-detail",
       pattern: "{meta}-{id}.html",
       defaults: new { controller = "Post", action = "Detail" });

    e.MapControllerRoute(
       name: "site-map",
       pattern: "site-map/sitemap.xml",
       defaults: new { controller = "Home", action = "SiteMap" });

    e.MapControllerRoute(
       name: "categories",
       pattern: "danh-muc/{meta}-{id}.html",
       defaults: new { controller = "Category", action = "Index" });

    e.MapControllerRoute(
       name: "tag",
       pattern: "tag/{meta}-{id}.html",
       defaults: new { controller = "Tag", action = "Index" });

    e.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
