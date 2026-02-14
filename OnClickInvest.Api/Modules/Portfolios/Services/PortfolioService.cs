using OnClickInvest.Api.Modules.Portfolios.DTOs;
using OnClickInvest.Api.Modules.Portfolios.Models;
using OnClickInvest.Api.Modules.Portfolios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Portfolios.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public async Task<PortfolioDTO> CreateAsync(Guid tenantId, PortfolioDTO dto)
        {
            var portfolio = new Portfolio
            {
                TenantId = tenantId,
                InvestorId = dto.InvestorId,
                Name = dto.Name,
                Description = dto.Description,
                InitialAmount = dto.InitialAmount
            };

            await _repository.CreateAsync(portfolio);

            dto.Id = portfolio.Id;
            return dto;
        }

        public async Task<List<PortfolioDTO>> GetAllAsync(Guid tenantId)
        {
            var portfolios = await _repository.GetAllAsync(tenantId);

            return portfolios.Select(p => new PortfolioDTO
            {
                Id = p.Id,
                InvestorId = p.InvestorId,
                Name = p.Name,
                Description = p.Description,
                InitialAmount = p.InitialAmount,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<List<PortfolioDTO>> GetByInvestorAsync(Guid tenantId, Guid investorId)
        {
            var portfolios = await _repository.GetByInvestorAsync(investorId, tenantId);

            return portfolios.Select(p => new PortfolioDTO
            {
                Id = p.Id,
                InvestorId = p.InvestorId,
                Name = p.Name,
                Description = p.Description,
                InitialAmount = p.InitialAmount,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<PortfolioDTO?> GetByIdAsync(Guid tenantId, Guid id)
        {
            var portfolio = await _repository.GetByIdAsync(id, tenantId);
            if (portfolio == null) return null;

            return new PortfolioDTO
            {
                Id = portfolio.Id,
                InvestorId = portfolio.InvestorId,
                Name = portfolio.Name,
                Description = portfolio.Description,
                InitialAmount = portfolio.InitialAmount,
                IsActive = portfolio.IsActive
            };
        }

        public async Task UpdateAsync(Guid tenantId, Guid id, PortfolioDTO dto)
        {
            var portfolio = await _repository.GetByIdAsync(id, tenantId)
                ?? throw new Exception("Portfolio não encontrado");

            portfolio.Name = dto.Name;
            portfolio.Description = dto.Description;
            portfolio.InitialAmount = dto.InitialAmount;
            portfolio.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(portfolio);
        }

        public async Task DisableAsync(Guid tenantId, Guid id)
        {
            var portfolio = await _repository.GetByIdAsync(id, tenantId)
                ?? throw new Exception("Portfolio não encontrado");

            portfolio.IsActive = false;
            portfolio.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(portfolio);
        }
    }
}
