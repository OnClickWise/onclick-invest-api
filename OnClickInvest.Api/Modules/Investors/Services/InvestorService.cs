using OnClickInvest.Api.Modules.Investors.DTOs;
using OnClickInvest.Api.Modules.Investors.Models;
using OnClickInvest.Api.Modules.Investors.Repositories;

namespace OnClickInvest.Api.Modules.Investors.Services
{
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository _repository;

        public InvestorService(IInvestorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InvestorDTO>> GetAllAsync(Guid tenantId)
        {
            var investors = await _repository.GetAllAsync(tenantId);

            return investors.Select(i => new InvestorDTO
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Document = i.Document,
                IsActive = i.IsActive
            });
        }

        public async Task<InvestorDTO?> GetByIdAsync(Guid id, Guid tenantId)
        {
            var investor = await _repository.GetByIdAsync(id, tenantId);
            if (investor == null) return null;

            return new InvestorDTO
            {
                Id = investor.Id,
                Name = investor.Name,
                Email = investor.Email,
                Document = investor.Document,
                IsActive = investor.IsActive
            };
        }

        public async Task<InvestorDTO> CreateAsync(Guid tenantId, InvestorDTO dto)
        {
            var investor = new Investor
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = dto.Name,
                Email = dto.Email,
                Document = dto.Document,
                IsActive = true
            };

            await _repository.AddAsync(investor);

            dto.Id = investor.Id;
            dto.IsActive = true;

            return dto;
        }

        public async Task UpdateAsync(Guid id, Guid tenantId, InvestorDTO dto)
        {
            var investor = await _repository.GetByIdAsync(id, tenantId)
                ?? throw new Exception("Investor not found");

            investor.Name = dto.Name;
            investor.Email = dto.Email;
            investor.Document = dto.Document;
            investor.IsActive = dto.IsActive;
            investor.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(investor);
        }

        public async Task DeleteAsync(Guid id, Guid tenantId)
        {
            var investor = await _repository.GetByIdAsync(id, tenantId)
                ?? throw new Exception("Investor not found");

            await _repository.DeleteAsync(investor);
        }
    }
}
