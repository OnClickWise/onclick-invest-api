using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using OnClickInvest.Api.Modules.Users.DTOs;
using OnClickInvest.Api.Modules.Users.DTOS;
using OnClickInvest.Api.Modules.Users.Enums;
using OnClickInvest.Api.Modules.Users.Models;
using OnClickInvest.Api.Modules.Users.Repositories;

namespace OnClickInvest.Api.Modules.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<List<UserResponseDto>> GetAllAsync(Guid tenantId)
        {
            var users = await _repository.GetByTenantAsync(tenantId);

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        public async Task<List<UserResponseDto>> GetAdminsAsync()
        {
            var users = await _repository.GetAdminsAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                TenantId = u.TenantId,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        public async Task<UserResponseDto> CreateAdminAsync(CreateAdminDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Role = UserRole.ADMIN,
                TenantId = dto.TenantId,
                IsActive = true
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(user, dto.Password);

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                TenantId = user.TenantId,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto> CreateInvestorAsync(
            Guid tenantId,
            CreateUserDto dto
        )
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Role = UserRole.INVESTOR,
                TenantId = tenantId,
                IsActive = true
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(user, dto.Password);

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task UpdateAsync(
            Guid tenantId,
            Guid userId,
            UpdateUserDto dto
        )
        {
            var user = await _repository.GetByIdAsync(userId, tenantId);

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            user.IsActive = dto.IsActive;

            await _repository.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(Guid userId, ChangePasswordDto dto)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            // Verifica se a senha atual está correta
            var verifyResult = _passwordHasher.VerifyHashedPassword(
                user, 
                user.PasswordHash, 
                dto.CurrentPassword
            );

            if (verifyResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Senha atual incorreta");

            // Atualiza para a nova senha
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.NewPassword);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProfileAsync(Guid userId, UpdateProfileDto dto)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            // Verifica se o email já está em uso por outro usuário
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null && existingUser.Id != userId)
                throw new InvalidOperationException("Email já está em uso");

            user.Email = dto.Email;

            await _repository.SaveChangesAsync();
        }
    }
}
