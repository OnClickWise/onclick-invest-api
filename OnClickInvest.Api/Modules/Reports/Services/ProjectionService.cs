using System;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Reports.DTOs;
using OnClickInvest.Api.Shared.Utils.Financial;

namespace OnClickInvest.Api.Modules.Reports.Services
{
    public class ProjectionService : IProjectionService
    {
        public async Task<ProjectionResponseDto> GenerateAsync(Guid tenantId, ProjectionRequestDto request)
        {
            var response = new ProjectionResponseDto();

            int totalMonths = request.Years * 12;
            for (int i = 1; i <= totalMonths; i++)
                response.Labels.Add($"Mês {i}");

            foreach (var scenarioDto in request.Scenarios)
            {
                var scenario = ProjectionEngine.Execute(
                    scenarioDto.Name,
                    scenarioDto.AnnualRate,
                    request.InitialCapital,
                    request.MonthlyContribution,
                    request.Years
                );

                var finalSnapshot = scenario.Snapshots.Last();

                response.Scenarios.Add(new ScenarioResultDto
                {
                    Name = scenario.Name,
                    Data = scenario.Snapshots.Select(s => s.TotalAmount).ToList(),
                    Kpis = new ProjectionKpiDto
                    {
                        TotalInvested = finalSnapshot.TotalInvested,
                        FinalAmount = finalSnapshot.TotalAmount,
                        TotalProfit = finalSnapshot.Profit,
                        ProfitabilityPercent =
                            finalSnapshot.TotalInvested == 0
                            ? 0
                            : (finalSnapshot.Profit / finalSnapshot.TotalInvested) * 100
                    }
                });
            }

            return await Task.FromResult(response);
        }
    }
}
