using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Plans.Repositories;
using OnClickInvest.Api.Modules.Plans.Services;
using OnClickInvest.Api.Modules.Subscriptions.Repositories;
using OnClickInvest.Api.Modules.Subscriptions.Services;
using OnClickInvest.Api.Modules.Investors.Repositories;
using OnClickInvest.Api.Modules.Investors.Services;
using OnClickInvest.Api.Modules.Portfolios.Repositories;
using OnClickInvest.Api.Modules.Portfolios.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Configuration
// =====================================================
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

// =====================================================
// Controllers & Swagger
// =====================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================================
// Database
// =====================================================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not configured");

    options.UseNpgsql(connectionString);
});

// =====================================================
// JWT Authentication
// =====================================================
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT Secret not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret)
            )
        };
    });

builder.Services.AddAuthorization();

// =====================================================
// Dependency Injection
// =====================================================

// Plans
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();

// Subscriptions
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// Investors
builder.Services.AddScoped<IInvestorService, InvestorService>();
builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();

// 🔥 Portfolios & Investments (NOVO)
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

var app = builder.Build();

// =====================================================
// Pipeline
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =====================================================
// Apply Migrations
// =====================================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        Console.WriteLine("[DB] ✅ Migrations aplicadas.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB] ⚠ Erro ao aplicar migrations: {ex.Message}");
    }
}

app.Run();
