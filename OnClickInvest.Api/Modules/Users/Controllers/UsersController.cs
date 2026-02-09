using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Users.DTOs;
using OnClickInvest.Api.Modules.Users.DTOS;
using OnClickInvest.Api.Modules.Users.Services;

namespace OnClickInvest.Api.Modules.Users.Controllers
{
    [ApiController]
    [Route("admin/users")]
    [Authorize(Roles = "ADMIN")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        private Guid GetTenantId()
        {
            var tenantId = User.FindFirst("tenantId")?.Value;
            if (string.IsNullOrEmpty(tenantId))
                throw new UnauthorizedAccessException();

            return Guid.Parse(tenantId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync(GetTenantId());
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = await _service.CreateInvestorAsync(GetTenantId(), dto);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            await _service.UpdateAsync(GetTenantId(), id, dto);
            return NoContent();
        }
    }
}
