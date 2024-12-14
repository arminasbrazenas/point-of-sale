using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserValidationService
{
    void ValidateApplicationUserRole(string role);
    Task ValidateRegisterApplicationUserDTO(RegisterApplicationUserDTO dto);
}
