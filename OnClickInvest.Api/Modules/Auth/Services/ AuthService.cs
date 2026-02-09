using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Auth.DTOs;
using OnClickInvest.Api.Modules.Users.Enums;
using OnClickInvest.Api.Modules.Users.Models;
using OnClickInvest.Api.Modules.Tenancy.Models;

namespace OnClickInvest.Api.Modules.Auth.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> RegisterTenantAsync(RegisterTenantDto dto)
        {
            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.AdminEmail);
            if (emailExists) throw new InvalidOperationException("Email já registrado");

            var tenant = new Tenant { Name = dto.OrganizationName };
            var admin = new User
            {
                Email = dto.AdminEmail,
                Role = UserRole.ADMIN,
                Tenant = tenant, // EF seta TenantId
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Tenants.Add(tenant);
            _context.Users.Add(admin);

            var refreshToken = _tokenService.GenerateRefreshToken(admin.Id);
            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = _tokenService.GenerateAccessToken(admin),
                RefreshToken = refreshToken.Token,
                User = new UserMeDto
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    Role = admin.Role,
                    TenantId = admin.TenantId ?? Guid.Empty
                }
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.IsActive);
            if (user == null) throw new UnauthorizedAccessException("Credenciais inválidas");

            var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!validPassword) throw new UnauthorizedAccessException("Credenciais inválidas");

            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.Token,
                User = new UserMeDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    TenantId = user.TenantId ?? Guid.Empty
                }
            };
        }

        public async Task<UserMeDto> GetMeAsync(Guid userId)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new UnauthorizedAccessException();

            return new UserMeDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                TenantId = user.TenantId ?? Guid.Empty
            };
        }
    }
}
