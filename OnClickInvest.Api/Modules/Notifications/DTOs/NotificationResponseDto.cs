using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;

namespace OnClickInvest.Api.Modules.Notifications.DTOs
{
    public class NotificationResponseDto
    {
        public Guid Id { get; set; }
         public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public Guid TenantId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? InvestorId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
