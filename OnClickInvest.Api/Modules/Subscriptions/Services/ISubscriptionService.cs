using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Subscriptions.DTOs;

namespace OnClickInvest.Api.Modules.Subscriptions.Services
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionDto>> GetAllAsync();
        Task<SubscriptionDto?> GetByTenantIdAsync(Guid tenantId);
        Task<SubscriptionDto> CreateAsync(SubscriptionDto dto);
        Task CancelAsync(Guid subscriptionId);
    }
}
