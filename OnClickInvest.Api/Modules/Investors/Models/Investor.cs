using System;

namespace OnClickInvest.Api.Modules.Investors.Models
{
    public class Investor
    {
        public Guid Id { get; set; }

        // Multi-tenant
        public Guid TenantId { get; set; }

        // Dados do cliente final
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Document { get; set; } // CPF / ID externo

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
