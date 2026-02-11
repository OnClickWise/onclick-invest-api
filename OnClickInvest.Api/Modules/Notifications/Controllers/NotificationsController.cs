using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Notifications.DTOs;
using OnClickInvest.Api.Modules.Notifications.Services;

namespace OnClickInvest.Api.Modules.Notifications.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost("{tenantId}")]
        public async Task<IActionResult> Create(Guid tenantId, NotificationCreateDto dto)
        {
            await _service.CreateAsync(tenantId, dto);
            return Ok();
        }

        [HttpGet("{tenantId}/investor/{investorId}")]
        public async Task<IActionResult> GetInvestor(Guid tenantId, Guid investorId)
        {
            var result = await _service.GetInvestorNotificationsAsync(tenantId, investorId);
            return Ok(result);
        }
    }
}
