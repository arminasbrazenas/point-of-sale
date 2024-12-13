using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserService
{
    Task<ApplicationUserDTO> CreateApplicationUser(RegisterApplicationUserDTO dto);
    Task<ApplicationUserDTO> GetApplicationUserByEmail(string email);
    Task<List<ApplicationUserDTO>> GetApplicationUsers();
    Task<TokensDTO> AuthenticateApplicationUser(LoginApplicationUserDTO dto);
    Task<ApplicationUserDTO> GetApplicationUserById(int id);
    Task<ApplicationUserDTO> GetCurrentApplicationUser();
}
