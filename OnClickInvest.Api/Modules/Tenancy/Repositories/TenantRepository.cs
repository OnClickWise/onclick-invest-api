
using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Tenancy.Models;

namespace OnClickInvest.Api.Modules.Tenancy.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tenant>> GetAllAsync()
            => await _context.Tenants.AsNoTracking().ToListAsync();

        public async Task<Tenant?> GetByIdAsync(Guid id)
            => await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tenant tenant)
        {
            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();
        }
    }
}
