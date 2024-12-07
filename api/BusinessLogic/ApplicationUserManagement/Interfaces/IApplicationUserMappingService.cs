using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserMappingService
{
    public ApplicationUserDTO MapApplicationUserDTO(ApplicationUser user, string role);
    public TokensDTO MapTokensDTO(string accessToken, string refreshToken);
}
