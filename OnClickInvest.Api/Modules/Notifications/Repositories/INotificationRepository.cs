using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Notifications.Models;

namespace OnClickInvest.Api.Modules.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetByUserAsync(Guid tenantId, Guid userId);
        Task<List<Notification>> GetByInvestorAsync(Guid tenantId, Guid investorId);
        Task<Notification?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
