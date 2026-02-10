using System;

namespace OnClickInvest.Api.Modules.Subscriptions.DTOs
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public bool IsActive { get; set; }
    }
}
