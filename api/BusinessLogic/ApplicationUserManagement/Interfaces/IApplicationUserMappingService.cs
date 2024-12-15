using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IApplicationUserMappingService
{
    public ApplicationUserDTO MapApplicationUserDTO(ApplicationUser user, string role);
    public TokensDTO MapTokensDTO(string accessToken, string refreshToken);
    public PagedResponseDTO<ApplicationUserDTO> MapPagedApplicationUserDTOs(
        List<(ApplicationUser User, string Role)> usersWithRoles,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
