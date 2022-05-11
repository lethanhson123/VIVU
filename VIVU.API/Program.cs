



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CreateBlogCommand).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.RegisterDatabaseCore(builder.Configuration);

var app = builder.Build();

/// <summary>
/// Migrate database
/// </summary>
using (var scope = app.Services.CreateScope())
{
    var applicationDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();    

    

    await applicationDb.Database.MigrateAsync();    

    if (app.Environment.IsDevelopment())
    {
        await ApplicationDatabaseInitializer.InitializeAsync(applicationDb);        
    }
}

app.UseCors("CORS");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();

