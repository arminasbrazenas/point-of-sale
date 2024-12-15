using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
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
            user.PhoneNumber!,
            user.Business?.Id,
            user.Business?.Name,
            role
        );
    }

    public TokensDTO MapTokensDTO(string accessToken, string refreshToken)
    {
        return new TokensDTO(accessToken, refreshToken);
    }

    public PagedResponseDTO<ApplicationUserDTO> MapPagedApplicationUserDTOs(
        List<(ApplicationUser User, string Role)> usersWithRoles,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<ApplicationUserDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = usersWithRoles.Select(x => MapApplicationUserDTO(x.User, x.Role)).ToList(),
        };
    }
}
