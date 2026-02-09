using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Tenancy.DTOs;
using OnClickInvest.Api.Modules.Tenancy.Repositories;
using OnClickInvest.Api.Modules.Tenancy.Models;

namespace OnClickInvest.Api.Modules.Tenancy.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _repository;

        public TenantService(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TenantResponseDto>> GetAllAsync()
        {
            var tenants = await _repository.GetAllAsync();

            return tenants.Select(t => new TenantResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                IsActive = t.IsActive,
                CreatedAt = t.CreatedAt
            }).ToList();
        }

        public async Task<TenantResponseDto> CreateAsync(CreateTenantDto dto)
        {
            var tenant = new Tenant
            {
                Name = dto.Name
            };

            await _repository.AddAsync(tenant);

            return new TenantResponseDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                IsActive = tenant.IsActive,
                CreatedAt = tenant.CreatedAt
            };
        }

        public async Task<TenantResponseDto> UpdateAsync(Guid id, UpdateTenantDto dto)
        {
            var tenant = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Tenant não encontrado");

            tenant.Name = dto.Name;
            tenant.IsActive = dto.IsActive;
            tenant.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(tenant);

            return new TenantResponseDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                IsActive = tenant.IsActive,
                CreatedAt = tenant.CreatedAt
            };
        }

        public async Task ToggleActiveAsync(Guid id, bool isActive)
        {
            var tenant = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Tenant não encontrado");

            tenant.IsActive = isActive;
            tenant.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(tenant);
        }
    }
}
