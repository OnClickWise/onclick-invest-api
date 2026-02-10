using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Portfolios.DTOs;
using OnClickInvest.Api.Modules.Portfolios.Services;
using System;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Portfolios.Controllers
{
    [ApiController]
    [Route("api/portfolios")]
    [Authorize(Roles = "ADMIN")]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _service;

        public PortfoliosController(IPortfolioService service)
        {
            _service = service;
        }

        // OBS: futuramente isso virá do Middleware de Tenancy
        private Guid TenantId
        {
            get
            {
                if (!HttpContext.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantIdValue))
                    throw new UnauthorizedAccessException("TenantId não informado no header.");

                return Guid.Parse(tenantIdValue!);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PortfolioDTO dto)
        {
            var result = await _service.CreateAsync(TenantId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("investor/{investorId}")]
        public async Task<IActionResult> GetByInvestor(Guid investorId)
        {
            var result = await _service.GetByInvestorAsync(TenantId, investorId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(TenantId, id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PortfolioDTO dto)
        {
            await _service.UpdateAsync(TenantId, id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _service.DisableAsync(TenantId, id);
            return NoContent();
        }
    }
}
