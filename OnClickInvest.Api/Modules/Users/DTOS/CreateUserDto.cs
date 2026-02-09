using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Users.DTOS
{
    public class CreateUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}