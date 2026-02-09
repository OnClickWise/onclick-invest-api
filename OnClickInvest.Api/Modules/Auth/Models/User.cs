using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Auth.Models;


namespace OnClickInvest.Api.Models
{
    public enum UserRole
    {
        SUPER_ADMIN,
        ADMIN,
        INVESTOR
    }

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? TenantId { get; set; }

        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Tenant? Tenant { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
