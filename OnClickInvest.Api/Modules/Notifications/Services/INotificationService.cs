using OnClickInvest.Api.Modules.Notifications.DTOs;

namespace OnClickInvest.Api.Modules.Notifications.Services
{
    public interface INotificationService
    {
        // Criação genérica via DTO
        Task CreateAsync(Guid tenantId, NotificationCreateDto dto);

        // Criação direta para User
        Task CreateForUserAsync(Guid tenantId, Guid userId, string title, string message);

        // Criação direta para Investor
        Task CreateForInvestorAsync(Guid tenantId, Guid investorId, string title, string message);

        // Buscar notificações do investidor
        Task<List<NotificationResponseDto>> GetInvestorNotificationsAsync(Guid tenantId, Guid investorId);

        // Marcar como lida
        Task MarkAsReadAsync(Guid notificationId);
    }
}
