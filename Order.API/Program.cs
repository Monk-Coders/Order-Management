using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Order.Repository.Context;
using Order.Repository.Repository;
using Order.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddConsole(options =>
{
    options.FormatterName = ConsoleFormatterNames.Simple;
});

builder.Logging.AddSimpleConsole(options =>
{
    options.UseUtcTimestamp = false; // optional
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ";
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Entity Framework with PostgreSQL
builder.Services.AddDbContext<OrderDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

    // Enable SQL query logging in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.LogTo(
            log => Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {log}"),
            LogLevel.Information);
    }
});

// Register repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISalesAnalysisRepository, SalesAnalysisRepository>();
builder.Services.AddScoped<ICsvLoaderRepository, CsvLoaderRepository>();

// Register services
builder.Services.AddScoped<ISalesAnalysisService, SalesAnalysisService>();
builder.Services.AddScoped<IDataRefreshService, DataRefreshService>();


// Register memory cache
builder.Services.AddMemoryCache();


var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseRouting();

app.MapControllers();

app.Run();
