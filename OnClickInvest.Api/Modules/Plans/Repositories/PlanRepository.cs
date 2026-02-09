using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Plans.Models;

namespace OnClickInvest.Api.Modules.Plans.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Plan?> GetByIdAsync(Guid id)
        {
            return await _context.Plans
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Plan>> GetAllAsync()
        {
            return await _context.Plans
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }

        public Task UpdateAsync(Plan plan)
        {
            _context.Plans.Update(plan);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
