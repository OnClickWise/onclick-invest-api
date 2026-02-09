using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public UserMeDto User { get; set; } = null!;
    }
}
