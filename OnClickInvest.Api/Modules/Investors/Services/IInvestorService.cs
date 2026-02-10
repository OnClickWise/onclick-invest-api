using OnClickInvest.Api.Modules.Investors.DTOs;

namespace OnClickInvest.Api.Modules.Investors.Services
{
    public interface IInvestorService
    {
        Task<IEnumerable<InvestorDTO>> GetAllAsync(Guid tenantId);
        Task<InvestorDTO?> GetByIdAsync(Guid id, Guid tenantId);
        Task<InvestorDTO> CreateAsync(Guid tenantId, InvestorDTO dto);
        Task UpdateAsync(Guid id, Guid tenantId, InvestorDTO dto);
        Task DeleteAsync(Guid id, Guid tenantId);
    }
}
