using System;
using OnClickInvest.Api.Modules.Plans.Models;
using OnClickInvest.Api.Modules.Tenancy.Models;

namespace OnClickInvest.Api.Modules.Subscriptions.Models
{
    public class Subscription
    {
        public Guid Id { get; private set; }

        public Guid TenantId { get; private set; }
        public Tenant Tenant { get; private set; } = null!;

        public Guid PlanId { get; private set; }
        public Plan Plan { get; private set; } = null!;

        public DateTime StartAt { get; private set; }
        public DateTime? EndAt { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? CanceledAt { get; private set; }

        protected Subscription() { } // EF Core

        public Subscription(Guid tenantId, Guid planId, DateTime startAt)
        {
            Id = Guid.NewGuid();
            TenantId = tenantId;
            PlanId = planId;
            StartAt = startAt;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (!IsActive) return;

            IsActive = false;
            CanceledAt = DateTime.UtcNow;
            EndAt = CanceledAt;
        }

        public void ChangePlan(Guid newPlanId)
        {
            PlanId = newPlanId;
        }
    }
}
