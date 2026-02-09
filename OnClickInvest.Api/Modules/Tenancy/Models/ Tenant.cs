using System;
using System.Collections.Generic;
using OnClickInvest.Api.Modules.Users.Models;

namespace OnClickInvest.Api.Modules.Tenancy.Models
{
    public class Tenant
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Nunca nulo, controlado pelo backend
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
