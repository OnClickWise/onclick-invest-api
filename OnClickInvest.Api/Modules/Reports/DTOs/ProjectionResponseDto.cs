using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnClickInvest.Api.Modules.Reports.DTOs
{
    public class ProjectionResponseDto
    {
        public List<string> Labels { get; set; } = new();
        public List<ScenarioResultDto> Scenarios { get; set; } = new();
    }

    public class ScenarioResultDto
    {
        public string Name { get; set; } = string.Empty;
        public List<decimal> Data { get; set; } = new();
        public ProjectionKpiDto Kpis { get; set; } = new();
    }
}
