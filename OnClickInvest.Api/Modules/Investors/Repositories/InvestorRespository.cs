using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Investors.Models;

namespace OnClickInvest.Api.Modules.Investors.Repositories
{
    public class InvestorRepository : IInvestorRepository
    {
        private readonly AppDbContext _context;

        public InvestorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Investor?> GetByIdAsync(Guid id, Guid tenantId)
        {
            return await _context.Set<Investor>()
                .FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);
        }

        public async Task<IEnumerable<Investor>> GetAllAsync(Guid tenantId)
        {
            return await _context.Set<Investor>()
                .Where(i => i.TenantId == tenantId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Investor investor)
        {
            await _context.Set<Investor>().AddAsync(investor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Investor investor)
        {
            _context.Set<Investor>().Update(investor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Investor investor)
        {
            _context.Set<Investor>().Remove(investor);
            await _context.SaveChangesAsync();
        }
    }
}
