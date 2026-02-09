using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Auth.DTOs
{
    public class RegisterTenantDto
    {
        public string OrganizationName { get; set; } = null!;
        public string AdminEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
