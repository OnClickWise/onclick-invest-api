using System.ComponentModel.DataAnnotations;

namespace OnClickInvest.Api.Modules.Users.DTOS
{
    public class UpdateProfileDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
