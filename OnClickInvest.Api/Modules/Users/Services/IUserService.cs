using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using OnClickInvest.Api.Modules.Users.DTOs;
using OnClickInvest.Api.Modules.Users.DTOS;




namespace OnClickInvest.Api.Modules.Users.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(Guid tenantId);

        Task<List<UserResponseDto>> GetAdminsAsync();

        Task<UserResponseDto> CreateAdminAsync(CreateAdminDto dto);

        Task<UserResponseDto> CreateInvestorAsync(
            Guid tenantId,
            CreateUserDto dto
        );

        Task UpdateAsync(
            Guid tenantId,
            Guid userId,
            UpdateUserDto dto
        );
    }
}
