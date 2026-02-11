using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Reports.DTOs
{
    public class ProjectionKpiDto
    {
        public decimal TotalInvested { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal ProfitabilityPercent { get; set; }
    }
}
