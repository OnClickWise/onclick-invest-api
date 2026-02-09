using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OnClickInvest.Api.Modules.Tenancy.DTOs;

namespace OnClickInvest.Api.Modules.Tenancy.Services
{
    public interface ITenantService
    {
        Task<List<TenantResponseDto>> GetAllAsync();
        Task<TenantResponseDto> CreateAsync(CreateTenantDto dto);
        Task<TenantResponseDto> UpdateAsync(Guid id, UpdateTenantDto dto);
        Task ToggleActiveAsync(Guid id, bool isActive);
    }
}
