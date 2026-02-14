using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Portfolios.Models;

namespace OnClickInvest.Api.Modules.Portfolios.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAllAsync(Guid tenantId);
        Task<Portfolio?> GetByIdAsync(Guid id, Guid tenantId);
        Task<List<Portfolio>> GetByInvestorAsync(Guid investorId, Guid tenantId);
        Task CreateAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(Portfolio portfolio);
    }
}
