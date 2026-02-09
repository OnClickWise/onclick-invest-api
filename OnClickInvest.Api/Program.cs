using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnClickInvest.Api.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Configuration - Apenas appsettings.json
// =====================================================
// Removido o .AddEnvironmentVariables() se não estiver usando Docker/Cloud agora
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
// Database - PostgreSQL (Otimizado para Dev)
// =====================================================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not configured");
    
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        // Reduzimos o retry para não "travar" o terminal se o banco estiver off
        npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 2);
    });
});

// =====================================================
// JWT Authentication
// =====================================================
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT Secret not configured");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// =====================================================
// Dependency Injection (Organizada)
// =====================================================
// Auth
builder.Services.AddScoped<OnClickInvest.Api.Modules.Auth.Services.AuthService>();
builder.Services.AddScoped<OnClickInvest.Api.Modules.Auth.Services.TokenService>();

// Tenancy
builder.Services.AddScoped<
    OnClickInvest.Api.Modules.Tenancy.Repositories.ITenantRepository,
    OnClickInvest.Api.Modules.Tenancy.Repositories.TenantRepository>();
builder.Services.AddScoped<
    OnClickInvest.Api.Modules.Tenancy.Services.ITenantService,
    OnClickInvest.Api.Modules.Tenancy.Services.TenantService>();

// Users
builder.Services.AddScoped<
    OnClickInvest.Api.Modules.Users.Repositories.IUserRepository,
    OnClickInvest.Api.Modules.Users.Repositories.UserRepository>();
builder.Services.AddScoped<
    OnClickInvest.Api.Modules.Users.Services.IUserService,
    OnClickInvest.Api.Modules.Users.Services.UserService>();

var app = builder.Build();

// =====================================================
// Pipeline de Execução
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Em desenvolvimento local, se não tiver certificado HTTPS, pode comentar a linha abaixo
// app.UseHttpsRedirection(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();