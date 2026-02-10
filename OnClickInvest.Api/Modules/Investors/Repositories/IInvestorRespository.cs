using OnClickInvest.Api.Modules.Investors.Models;

namespace OnClickInvest.Api.Modules.Investors.Repositories
{
    public interface IInvestorRepository
    {
        Task<Investor?> GetByIdAsync(Guid id, Guid tenantId);
        Task<IEnumerable<Investor>> GetAllAsync(Guid tenantId);
        Task AddAsync(Investor investor);
        Task UpdateAsync(Investor investor);
        Task DeleteAsync(Investor investor);
    }
}
