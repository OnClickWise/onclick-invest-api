using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnClickInvest.Api.Modules.Notifications.DTOs
{
    public class NotificationCreateDto
    {
        public Guid? UserId { get; set; }          // para ADMIN / usuário interno
        public Guid? InvestorId { get; set; }      // para INVESTOR
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "INFO";
    }
}
