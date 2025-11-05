using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CordysCRM.Framework.Data;
using CordysCRM.Framework.Repositories;
using CordysCRM.Framework.Security;
using CordysCRM.Framework.Storage;
using CordysCRM.Framework.Middleware;
using CordysCRM.CRM.Repositories;
using CordysCRM.CRM.Services;
using CordysCRM.App.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add HttpContextAccessor for session management
builder.Services.AddHttpContextAccessor();

// Configure MySQL Database with Pomelo
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connectionString))
{
    // Register CrmApplicationDbContext
    builder.Services.AddDbContext<CrmApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    
    // Also register as DbContext so repositories can use it
    builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<CrmApplicationDbContext>());
}

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<CrmApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure Authentication
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Register file storage service
builder.Services.AddSingleton<IFileStorageService>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<LocalFileStorageService>>();
    var basePath = Path.Combine("/opt/cordys/files");
    return new LocalFileStorageService(logger, basePath);
});

// Add API documentation (Swagger/OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Redis Session Management
var redisConnection = builder.Configuration.GetConnectionString("Redis");
if (!string.IsNullOrEmpty(redisConnection))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnection;
    });

    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromHours(12); // 43200s = 12 hours
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
}

// Add HTTP Response Compression (gzip)
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = new[]
    {
        "application/json",
        "application/xml",
        "text/html",
        "text/xml",
        "text/plain",
        "application/javascript",
        "text/css",
        "text/javascript",
        "image/jpeg"
    };
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
// Add custom middleware (AOP-style)
app.UseExceptionHandling();
app.UseRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

if (!string.IsNullOrEmpty(redisConnection))
{
    app.UseSession();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
