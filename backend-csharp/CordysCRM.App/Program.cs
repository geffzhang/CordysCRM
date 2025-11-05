using Microsoft.EntityFrameworkCore;
using CordysCRM.Framework.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add HttpContextAccessor for session management
builder.Services.AddHttpContextAccessor();

// Add API documentation (Swagger/OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MySQL Database with Pomelo
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

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

app.UseAuthorization();

app.MapControllers();

app.Run();
