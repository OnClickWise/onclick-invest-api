using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Subscriptions.DTOs;
using OnClickInvest.Api.Modules.Subscriptions.Services;

namespace OnClickInvest.Api.Modules.Subscriptions.Controllers
{
    [ApiController]
    [Route("api/admin/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _service;

        public SubscriptionsController(ISubscriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> GetByTenant(Guid tenantId)
        {
            var subscription = await _service.GetByTenantIdAsync(tenantId);
            return subscription == null ? NotFound() : Ok(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionDto dto)
        {
            var subscription = await _service.CreateAsync(dto);
            return Ok(subscription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _service.CancelAsync(id);
            return NoContent();
        }
    }
}
