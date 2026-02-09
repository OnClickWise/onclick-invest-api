using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Users.Enums;



namespace OnClickInvest.Api.Modules.Users.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }
        public Guid? TenantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
