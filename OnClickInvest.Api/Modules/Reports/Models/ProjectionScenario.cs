using System;
using System.Collections.Generic;

namespace OnClickInvest.Api.Modules.Reports.Models
{
    public class ProjectionScenario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProjectionId { get; set; }
        public Projection Projection { get; set; } = null!;

        public string Name { get; set; } = null!;

        // Taxa anual (ex: 8.5 = 8.5%)
        public decimal AnnualRate { get; set; }

        // Navigation
        public ICollection<ProjectionSnapshot> Snapshots { get; set; }
            = new List<ProjectionSnapshot>();
    }
}
