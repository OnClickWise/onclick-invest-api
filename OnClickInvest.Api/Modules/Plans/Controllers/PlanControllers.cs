using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Plans.DTOs;
using OnClickInvest.Api.Modules.Plans.Services;

namespace OnClickInvest.Api.Modules.Plans.Controllers
{
    [ApiController]
    [Route("api/plans")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _service;

        public PlanController(IPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var plan = await _service.GetByIdAsync(id);
            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanDto dto)
        {
            var plan = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlanDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _service.DeactivateAsync(id);
            return NoContent();
        }
    }
}
