using System;
using System.Collections.Generic;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Auth.Models;
using OnClickInvest.Api.Modules.Users.Enums;

namespace OnClickInvest.Api.Modules.Users.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nullable para permitir SUPER_ADMIN
        public Guid? TenantId { get; set; }

        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navegação
        public Tenant? Tenant { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
