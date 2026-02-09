using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Plans.DTOs;

namespace OnClickInvest.Api.Modules.Plans.Services
{
    public interface IPlanService
    {
        Task<List<PlanDto>> GetAllAsync();
        Task<PlanDto?> GetByIdAsync(Guid id);
        Task<PlanDto> CreateAsync(PlanDto dto);
        Task UpdateAsync(Guid id, PlanDto dto);
        Task DeactivateAsync(Guid id);
    }
}
