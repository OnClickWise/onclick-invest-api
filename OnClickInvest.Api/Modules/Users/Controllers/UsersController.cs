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
    [Authorize]
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync(GetTenantId());
            return Ok(users);
        }

        [HttpGet("admins")]
        [Authorize(Roles = "SUPER_ADMIN")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _service.GetAdminsAsync();
            return Ok(admins);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = await _service.CreateInvestorAsync(GetTenantId(), dto);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpPost("admins")]
        [Authorize(Roles = "SUPER_ADMIN")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            var user = await _service.CreateAdminAsync(dto);
            return CreatedAtAction(nameof(GetAdmins), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            await _service.UpdateAsync(GetTenantId(), id, dto);
            return NoContent();
        }
    }
}
