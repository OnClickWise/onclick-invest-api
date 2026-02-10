using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Subscriptions.Models;

namespace OnClickInvest.Api.Modules.Subscriptions.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription?> GetByIdAsync(Guid id)
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Subscription?> GetActiveByTenantIdAsync(Guid tenantId)
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s =>
                    s.TenantId == tenantId && s.IsActive);
        }

        public async Task<List<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
