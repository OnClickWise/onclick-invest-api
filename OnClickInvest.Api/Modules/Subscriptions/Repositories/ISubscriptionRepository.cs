using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Subscriptions.Models;

namespace OnClickInvest.Api.Modules.Subscriptions.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(Guid id);
        Task<Subscription?> GetActiveByTenantIdAsync(Guid tenantId);
        Task<List<Subscription>> GetAllAsync();
        Task AddAsync(Subscription subscription);
        Task SaveChangesAsync();
    }
}
