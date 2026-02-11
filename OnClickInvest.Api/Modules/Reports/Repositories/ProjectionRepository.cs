using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Reports.Repositories
{
    public class ProjectionRepository : IProjectionRepository
    {
        public Task SaveSnapshotAsync(Guid tenantId, Guid investorId, string scenarioName, decimal finalAmount)
        {
            // Futuro: persistir snapshot histórico
            return Task.CompletedTask;
        }
    }
}
