using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;

namespace OnClickInvest.Api.Modules.Reports.DTOs
{
    public class ProjectionRequestDto
    {
        [Required]
        public Guid InvestorId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InitialCapital { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MonthlyContribution { get; set; }

        [Range(1, 100)]
        public int Years { get; set; }

        public List<ScenarioDto> Scenarios { get; set; } = new();
    }
}
