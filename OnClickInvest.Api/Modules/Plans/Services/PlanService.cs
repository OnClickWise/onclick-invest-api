using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Plans.DTOs;
using OnClickInvest.Api.Modules.Plans.Models;
using OnClickInvest.Api.Modules.Plans.Repositories;

namespace OnClickInvest.Api.Modules.Plans.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _repository;

        public PlanService(IPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PlanDto>> GetAllAsync()
        {
            var plans = await _repository.GetAllAsync();

            return plans.Select(MapToDto).ToList();
        }

        public async Task<PlanDto?> GetByIdAsync(Guid id)
        {
            var plan = await _repository.GetByIdAsync(id);
            return plan == null ? null : MapToDto(plan);
        }

        public async Task<PlanDto> CreateAsync(PlanDto dto)
        {
            var plan = new Plan(
                dto.Name,
                dto.Description,
                dto.Price,
                dto.MaxUsers
            );

            await _repository.AddAsync(plan);
            await _repository.SaveChangesAsync();

            return MapToDto(plan);
        }

        public async Task UpdateAsync(Guid id, PlanDto dto)
        {
            var plan = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Plano não encontrado");

            plan.Update(
                dto.Name,
                dto.Description,
                dto.Price,
                dto.MaxUsers
            );

            await _repository.UpdateAsync(plan);
            await _repository.SaveChangesAsync();
        }

        public async Task DeactivateAsync(Guid id)
        {
            var plan = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Plano não encontrado");

            plan.Deactivate();
            await _repository.SaveChangesAsync();
        }

        private static PlanDto MapToDto(Plan plan)
        {
            return new PlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                MaxUsers = plan.MaxUsers,
                IsActive = plan.IsActive
            };
        }
    }
}
