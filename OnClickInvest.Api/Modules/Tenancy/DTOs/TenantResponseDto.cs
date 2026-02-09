using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnClickInvest.Api.Modules.Tenancy.DTOs
{
    public class TenantResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
