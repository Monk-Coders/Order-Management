using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;

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
builder.Services.AddDbContext<VanamDbContext>(options =>
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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITreeMasterRepository, TreeMasterRepository>();
builder.Services.AddScoped<ITreeRequestRepository, TreeRequestRepository>();
builder.Services.AddScoped<IPlantingRecordRepository, PlantingRecordRepository>();
builder.Services.AddScoped<IProgressUpdateRepository, ProgressUpdateRepository>();
builder.Services.AddScoped<IServiceablePINCodesRepository, ServiceablePINCodesRepository>();
builder.Services.AddScoped<ITreePricingRepository, TreePricingRepository>();
builder.Services.AddScoped<ISponsorOrderRepository, SponsorOrderRepository>();
builder.Services.AddScoped<IUserOtpInfoRepository, UserOtpInfoRepository>();
builder.Services.AddScoped<IApplicationSettingRepository, ApplicationSettingRepository>();
builder.Services.AddScoped<IApplicationInviteRepository, ApplicationInviteRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITreeService, TreeService>();
builder.Services.AddScoped<IPlantingProgressService, PlantingProgressService>();
builder.Services.AddScoped<ITreePricingService, TreePricingService>();
builder.Services.AddScoped<ISponsorOrderService, SponsorOrderService>();
builder.Services.AddScoped<IReverseGeocodingService, ReverseGeocodingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Register HttpClient for CCAvenue service
builder.Services.AddHttpClient<ICCAvenueService, CCAvenueService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Register HttpClient for ReverseGeocodingService
builder.Services.AddHttpClient<ReverseGeocodingService>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "VanamApp/1.0 (https://vanam.com; contact@vanam.com)");
    client.Timeout = TimeSpan.FromSeconds(30);
});

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
