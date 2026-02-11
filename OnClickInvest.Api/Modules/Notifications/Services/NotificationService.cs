using OnClickInvest.Api.Modules.Notifications.Models;
using OnClickInvest.Api.Modules.Notifications.Repositories;
using OnClickInvest.Api.Modules.Notifications.DTOs;

namespace OnClickInvest.Api.Modules.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Guid tenantId, NotificationCreateDto dto)
        {
            var notification = new Notification
            {
                TenantId = tenantId,
                UserId = dto.UserId,
                InvestorId = dto.InvestorId,
                Title = dto.Title,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _repository.AddAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task CreateForUserAsync(Guid tenantId, Guid userId, string title, string message)
        {
            var notification = new Notification
            {
                TenantId = tenantId,
                UserId = userId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _repository.AddAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task CreateForInvestorAsync(Guid tenantId, Guid investorId, string title, string message)
        {
            var notification = new Notification
            {
                TenantId = tenantId,
                InvestorId = investorId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _repository.AddAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<NotificationResponseDto>> GetInvestorNotificationsAsync(Guid tenantId, Guid investorId)
        {
            var notifications = await _repository
                .GetByInvestorAsync(tenantId, investorId);

            return notifications.Select(n => new NotificationResponseDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt,
                ReadAt = n.ReadAt
            }).ToList();
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _repository.GetByIdAsync(notificationId);
            if (notification == null)
                return;

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();
        }
    }
}
