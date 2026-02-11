using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Reports.DTOs;

namespace OnClickInvest.Api.Modules.Reports.Services
{
    public interface IProjectionService
    {
        Task<ProjectionResponseDto> GenerateAsync(Guid tenantId, ProjectionRequestDto request);
    }
}
