using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Auth.DTOs;
using OnClickInvest.Api.Modules.Auth.Services;

namespace OnClickInvest.Api.Modules.Auth.Controllers
{
    [ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("register-tenant")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterTenant(RegisterTenantDto dto)
        => Ok(await _service.RegisterTenantAsync(dto));

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto dto)
        => Ok(await _service.LoginAsync(dto));

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        return Ok(await _service.GetMeAsync(userId));
    }
}

}
