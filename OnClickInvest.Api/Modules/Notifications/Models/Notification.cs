using System;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Users.Models;

namespace OnClickInvest.Api.Modules.Notifications.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

        // Pode ser para usuário interno (Admin)
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        // Pode ser para Investor (cliente final)
        public Guid? InvestorId { get; set; }
        public Modules.Investors.Models.Investor? Investor { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }
    }
}
