using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Reports.DTOs;
using OnClickInvest.Api.Modules.Reports.Services;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Reports.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "ADMIN,INVESTOR")]
    public class ReportsController : ControllerBase
    {
        private readonly IProjectionService _service;

        public ReportsController(IProjectionService service)
        {
            _service = service;
        }

        private Guid TenantId
        {
            get
            {
                if (!Guid.TryParse(Request.Headers["X-Tenant-Id"], out var tenantId))
                    throw new InvalidOperationException("TenantId inválido ou não informado.");

                return tenantId;
            }
        }

        [HttpPost("projection")]
        public async Task<IActionResult> GenerateProjection([FromBody] ProjectionRequestDto request)
        {
            var result = await _service.GenerateAsync(TenantId, request);
            return Ok(result);
        }
    }
}
