using System;

namespace OnClickInvest.Api.Modules.Users.DTOs
{
    public class CreateAdminDto
    {
        public Guid TenantId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
