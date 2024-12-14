using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserMappingService : IApplicationUserMappingService
{
    public ApplicationUserDTO MapApplicationUserDTO(ApplicationUser user, string role)
    {
        return new ApplicationUserDTO(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.Business?.Id,
            user.Business?.Name,
            role
        );
    }

    public TokensDTO MapTokensDTO(string accessToken, string refreshToken)
    {
        return new TokensDTO(accessToken, refreshToken);
    }
}
