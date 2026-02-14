using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Portfolios.DTOs;

namespace OnClickInvest.Api.Modules.Portfolios.Services
{
    public interface IPortfolioService
    {
        Task<PortfolioDTO> CreateAsync(Guid tenantId, PortfolioDTO dto);
        Task<List<PortfolioDTO>> GetAllAsync(Guid tenantId);
        Task<List<PortfolioDTO>> GetByInvestorAsync(Guid tenantId, Guid investorId);
        Task<PortfolioDTO?> GetByIdAsync(Guid tenantId, Guid id);
        Task UpdateAsync(Guid tenantId, Guid id, PortfolioDTO dto);
        Task DisableAsync(Guid tenantId, Guid id);
    }
}
