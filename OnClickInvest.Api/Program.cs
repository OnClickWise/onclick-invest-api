using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Plans.Repositories;
using OnClickInvest.Api.Modules.Plans.Services;
using OnClickInvest.Api.Modules.Subscriptions.Repositories;
using OnClickInvest.Api.Modules.Subscriptions.Services;
using OnClickInvest.Api.Modules.Investors.Repositories;
using OnClickInvest.Api.Modules.Investors.Services;
using OnClickInvest.Api.Modules.Portfolios.Repositories;
using OnClickInvest.Api.Modules.Portfolios.Services;
using OnClickInvest.Api.Modules.Reports.Repositories;
using OnClickInvest.Api.Modules.Reports.Services;
using OnClickInvest.Api.Modules.Notifications.Repositories;
using OnClickInvest.Api.Modules.Notifications.Services;
using OnClickInvest.Api.Shared.Extensions;
using OnClickInvest.Api.Shared.Middlewares;
using OnClickInvest.Api.Shared.Utils;

var builder = WebApplication.CreateBuilder(args);

//
// =====================================================
// CONFIGURATION (já carregada automaticamente pelo .NET)
// =====================================================
// NÃO precisamos reconstruir configuration manualmente.
// O CreateBuilder já carrega appsettings.json automaticamente.
//

//
// =====================================================
// CONTROLLERS & SWAGGER
// =====================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//
// =====================================================
// DATABASE
// =====================================================
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection not configured.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

//
// =====================================================
// SHARED INFRASTRUCTURE (JWT)
// =====================================================
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();

//
// =====================================================
// DOMAIN DEPENDENCIES
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

// Portfolios
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

// Reports
builder.Services.AddScoped<IProjectionService, ProjectionService>();
builder.Services.AddScoped<IProjectionRepository, ProjectionRepository>();

// Notifications
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Auth
builder.Services.AddScoped<OnClickInvest.Api.Modules.Auth.Services.AuthService>();
builder.Services.AddScoped<OnClickInvest.Api.Modules.Auth.Services.TokenService>();

// Utils
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

var app = builder.Build();

//
// =====================================================
// PIPELINE
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 Ordem correta para SaaS multi-tenant
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("Frontend");

app.UseAuthentication();

app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

// TEMPORÁRIO: Desabilitar AuditMiddleware enquanto corrigimos a migration
// app.UseMiddleware<AuditMiddleware>();

app.MapControllers();

//
// =====================================================
// APPLY MIGRATIONS (AUTO DATABASE SYNC)
// =====================================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        Console.WriteLine("[DB] ✅ Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB] ⚠ Migration error: {ex.Message}");
    }
}

app.Run();
