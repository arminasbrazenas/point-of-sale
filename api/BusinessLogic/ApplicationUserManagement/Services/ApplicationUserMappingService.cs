using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserMappingService : IApplicationUserMappingService
{
    public ApplicationUserDTO MapApplicationUserDTO(ApplicationUser user, string role)
    {
        Business? business;
        if (role == "BusinessOwner")
        {
            business = user.OwnedBusiness;
        }
        else if (role == "Employee")
        {
            business = user.EmployerBusiness;
        }
        else
        {
            business = null;
        }

        return new ApplicationUserDTO(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.PhoneNumber!,
            business?.Id,
            business?.Name,
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
