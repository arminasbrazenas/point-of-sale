using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserMappingService
{
    public ApplicationUserDTO MapApplicationUserDTO(ApplicationUser user, string role);
}
