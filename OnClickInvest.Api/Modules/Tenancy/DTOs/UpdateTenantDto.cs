using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnClickInvest.Api.Modules.Tenancy.DTOs
{
    public class UpdateTenantDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
