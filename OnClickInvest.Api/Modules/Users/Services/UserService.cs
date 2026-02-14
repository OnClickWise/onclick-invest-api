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
    }
}
