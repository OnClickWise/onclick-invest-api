using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Users.Models;

namespace OnClickInvest.Api.Modules.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetByTenantAsync(Guid tenantId)
        {
            return await _context.Users
                .Where(u => u.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid userId, Guid tenantId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Id == userId &&
                    u.TenantId == tenantId
                );
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
