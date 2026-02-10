using System;
using System.Collections.Generic;
using OnClickInvest.Api.Modules.Investors.Models;
using OnClickInvest.Api.Modules.Tenancy.Models;

namespace OnClickInvest.Api.Modules.Portfolios.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public Guid InvestorId { get; set; }
        public Investor? Investor { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal InitialAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // 🔥 CORREÇÃO DO ERRO
        public ICollection<Investment> Investments { get; set; } = new List<Investment>();
    }
}
