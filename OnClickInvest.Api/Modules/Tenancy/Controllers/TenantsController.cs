using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnClickInvest.Api.Modules.Tenancy.DTOs;
using OnClickInvest.Api.Modules.Tenancy.Services;

namespace OnClickInvest.Api.Modules.Tenancy.Controllers
{
    [ApiController]
    [Route("tenants")]
    [Authorize(Roles = "SUPER_ADMIN")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _service;

        public TenantsController(ITenantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTenantDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _service.ToggleActiveAsync(id, true);
            return NoContent();
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _service.ToggleActiveAsync(id, false);
            return NoContent();
        }
    }
}
