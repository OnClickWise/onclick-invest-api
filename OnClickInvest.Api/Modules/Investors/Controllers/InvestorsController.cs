using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Investors.DTOs;
using OnClickInvest.Api.Modules.Investors.Services;

namespace OnClickInvest.Api.Modules.Investors.Controllers
{
    [ApiController]
    [Route("api/investors")]
    [Authorize]
    public class InvestorsController : ControllerBase
    {
        private readonly IInvestorService _service;

        public InvestorsController(IInvestorService service)
        {
            _service = service;
        }

        // 🔐 depois você pode pegar isso do Middleware de Tenancy
        private Guid TenantId => Guid.Parse(User.FindFirst("tenantId")!.Value);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync(TenantId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id, TenantId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvestorDTO dto)
        {
            var result = await _service.CreateAsync(TenantId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InvestorDTO dto)
        {
            await _service.UpdateAsync(id, TenantId, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id, TenantId);
            return NoContent();
        }
    }
}
