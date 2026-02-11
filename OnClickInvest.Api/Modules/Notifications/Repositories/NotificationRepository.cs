using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Notifications.Models;

namespace OnClickInvest.Api.Modules.Notifications.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<List<Notification>> GetByUserAsync(Guid tenantId, Guid userId)
        {
            return await _context.Notifications
                .Where(n => n.TenantId == tenantId && n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetByInvestorAsync(Guid tenantId, Guid investorId)
        {
            return await _context.Notifications
                .Where(n => n.TenantId == tenantId && n.InvestorId == investorId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
