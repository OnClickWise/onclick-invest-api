using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Plans.DTOs
{
    public class PlanDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
        public int MaxUsers { get; set; }

        public bool IsActive { get; set; }
    }
}
