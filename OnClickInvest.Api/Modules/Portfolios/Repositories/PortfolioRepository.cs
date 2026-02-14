using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Portfolios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Portfolios.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _context;

        public PortfolioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio?> GetByIdAsync(Guid id, Guid tenantId)
        {
            return await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
        }

        public async Task<List<Portfolio>> GetAllAsync(Guid tenantId)
        {
            return await _context.Portfolios
                .Where(p => p.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<List<Portfolio>> GetByInvestorAsync(Guid investorId, Guid tenantId)
        {
            return await _context.Portfolios
                .Where(p => p.InvestorId == investorId && p.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task CreateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
        }
    }
}
