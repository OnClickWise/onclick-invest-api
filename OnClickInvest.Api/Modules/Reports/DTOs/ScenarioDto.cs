using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnClickInvest.Api.Modules.Reports.DTOs
{
    public class ScenarioDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal AnnualRate { get; set; }
    }
}
