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
        public async Task<IActionResult> RegisterTenant([FromBody] RegisterTenantDto dto)
        {
            try
            {
                // Log genérico para não dar erro de compilação
                Console.WriteLine("[DEBUG] Recebendo requisição para registrar um novo tenant.");

                var result = await _service.RegisterTenantAsync(dto);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                // ISSO VAI MOSTRAR O ERRO REAL NO SEU TERMINAL
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=================================================");
                Console.WriteLine("ERRO FATAL NO REGISTER-TENANT:");
                Console.WriteLine($"Mensagem: {ex.Message}");
                Console.WriteLine($"Detalhes (Stack Trace): {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine("=================================================");
                Console.ResetColor();

                // Retorna o erro detalhado para o Frontend (útil para debug)
                return StatusCode(500, new 
                { 
                    message = "Ocorreu um erro no servidor.", 
                    error = ex.Message, 
                    details = ex.InnerException?.Message 
                });
            }
        }

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