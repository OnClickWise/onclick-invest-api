using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Users.Models;


using OnClickInvest.Api.Modules.Users.Enums;

namespace OnClickInvest.Api.Modules.Auth.DTOs
{
    public class UserMeDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public UserRole Role { get; set; }
        public Guid TenantId { get; set; }
    }
}
