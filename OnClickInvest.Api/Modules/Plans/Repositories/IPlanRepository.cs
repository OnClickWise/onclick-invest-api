using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Plans.Models;

namespace OnClickInvest.Api.Modules.Plans.Repositories
{
    public interface IPlanRepository
    {
        Task<Plan?> GetByIdAsync(Guid id);
        Task<List<Plan>> GetAllAsync();
        Task AddAsync(Plan plan);
        Task UpdateAsync(Plan plan);
        Task SaveChangesAsync();
    }
}
