using System;

namespace OnClickInvest.Api.Modules.Portfolios.DTOs
{
    public class PortfolioDTO
    {
        public Guid Id { get; set; }

        public Guid InvestorId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal InitialAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
