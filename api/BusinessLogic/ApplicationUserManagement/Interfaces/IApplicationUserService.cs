using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserService
{
    Task<ApplicationUserDTO> CreateApplicationUser(RegisterApplicationUserDTO dto);
    Task<List<ApplicationUserDTO>> GetApplicationUsers();
}
