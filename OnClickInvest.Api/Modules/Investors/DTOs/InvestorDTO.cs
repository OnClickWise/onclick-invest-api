using System;

namespace OnClickInvest.Api.Modules.Investors.DTOs
{
    public class InvestorDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Document { get; set; }

        public bool IsActive { get; set; }
    }
}
