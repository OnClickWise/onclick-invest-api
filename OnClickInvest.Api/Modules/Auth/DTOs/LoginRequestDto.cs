using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Auth.DTOs
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
