using System;
using System.Collections.Generic;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Investors.Models;

namespace OnClickInvest.Api.Modules.Reports.Models
{
    public class Projection
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

        public Guid InvestorId { get; set; }
        public Investor Investor { get; set; } = null!;

        public decimal InitialCapital { get; set; }
        public decimal MonthlyContribution { get; set; }
        public int Years { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<ProjectionScenario> Scenarios { get; set; }
            = new List<ProjectionScenario>();
    }
}

