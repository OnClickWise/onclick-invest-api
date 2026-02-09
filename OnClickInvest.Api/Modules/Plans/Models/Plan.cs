using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnClickInvest.Api.Modules.Plans.Models
{
    public class Plan
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public decimal Price { get; private set; }
        public int MaxUsers { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected Plan() { } // EF Core

        public Plan(
            string name,
            string description,
            decimal price,
            int maxUsers
        )
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            MaxUsers = maxUsers;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string description,
            decimal price,
            int maxUsers
        )
        {
            Name = name;
            Description = description;
            Price = price;
            MaxUsers = maxUsers;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
