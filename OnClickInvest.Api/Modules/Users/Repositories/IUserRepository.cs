using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Users.Models;


namespace OnClickInvest.Api.Modules.Users.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetByTenantAsync(Guid tenantId);

        Task<User?> GetByIdAsync(Guid userId, Guid tenantId);

        Task AddAsync(User user);

        Task SaveChangesAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);

    }
}