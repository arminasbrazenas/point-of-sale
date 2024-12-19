using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserService
{
    Task<ApplicationUserDTO> CreateApplicationUser(RegisterApplicationUserDTO dto);
    Task<ApplicationUserDTO> GetApplicationUserByEmail(string email);
    Task<PagedResponseDTO<ApplicationUserDTO>> GetApplicationUsers(
        int? businessId,
        string? role,
        PaginationFilterDTO paginationFilterDTO
    );
    Task<TokensDTO> AuthenticateApplicationUser(LoginApplicationUserDTO dto);
    Task<ApplicationUserDTO> GetApplicationUserById(int id);
    Task<ApplicationUserDTO> GetCurrentApplicationUser();
    Task<ApplicationUserDTO> UpdateApplicationUser(
        int applicationUserId,
        UpdateApplicationUserDTO updateApplicationUserDTO
    );
    Task DeleteApplicationUser(int applicationUserId);
    Task<TokensDTO> RefreshApplicationUserTokens(string? refreshToken);
    Task CreateAdminUser(RegisterApplicationUserDTO dto);
}
