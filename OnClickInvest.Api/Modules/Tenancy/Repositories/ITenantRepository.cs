using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Tenancy.Models;


namespace OnClickInvest.Api.Modules.Tenancy.Repositories
{
    public interface ITenantRepository
    {
        
        Task<List<Tenant>> GetAllAsync();
        Task<Tenant?> GetByIdAsync(Guid id);
        Task AddAsync(Tenant tenant);
        Task UpdateAsync(Tenant tenant);
    }
}
