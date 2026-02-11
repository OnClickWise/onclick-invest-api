using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using OnClickInvest.Api.Modules.Reports.Models;

namespace OnClickInvest.Api.Shared.Utils.Financial
{
    public static class ProjectionEngine
    {
        public static ProjectionScenario Execute(
            string scenarioName,
            decimal annualRate,
            decimal initialCapital,
            decimal monthlyContribution,
            int years)
        {
            var scenario = new ProjectionScenario
            {
                Name = scenarioName,
                AnnualRate = annualRate
            };

            decimal monthlyRate = annualRate / 100m / 12m;
            int totalMonths = years * 12;

            decimal totalInvested = initialCapital;
            decimal currentAmount = initialCapital;

            for (int month = 1; month <= totalMonths; month++)
            {
                currentAmount = currentAmount * (1 + monthlyRate) + monthlyContribution;
                totalInvested += monthlyContribution;

                scenario.Snapshots.Add(new ProjectionSnapshot
                {
                    Date = DateTime.UtcNow.AddMonths(month),
                    TotalInvested = totalInvested,
                    TotalAmount = currentAmount
                });
            }

            return scenario;
        }
    }
}
