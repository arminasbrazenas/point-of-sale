using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Interfaces;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> GetUserByIdWithBusinessAsync(int id);
    Task<ApplicationUser?> GetUserByEmailWithBusinessAsync(string email);
    Task<List<(ApplicationUser User, string Role)>> GetAllUsersWithBusinessAsync(
        int? businessId,
        string? role,
        PaginationFilter paginationFilter
    );
    Task<int> GetTotalCountAsync(int? businessId);
    Task<List<ApplicationUser>> GetManyByIdsAsync(List<int> userIds);
}
