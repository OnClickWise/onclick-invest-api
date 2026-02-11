using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Audit.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? TenantId { get; set; }
        public Guid? UserId { get; set; }

        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;

        public string Action { get; set; } = null!; // CREATE | UPDATE | DELETE

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}