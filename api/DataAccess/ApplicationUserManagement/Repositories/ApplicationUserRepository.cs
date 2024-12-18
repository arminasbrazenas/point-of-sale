using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetUserByIdWithBusinessAsync(int id)
    {
        return await _context
            .Users.Include(u => u.OwnedBusiness)
            .Include(u => u.EmployerBusiness)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ApplicationUser?> GetUserByEmailWithBusinessAsync(string email)
    {
        return await _context
            .Users.Include(u => u.OwnedBusiness)
            .Include(u => u.EmployerBusiness)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<(ApplicationUser User, string Role)>> GetAllUsersWithBusinessAsync(
        int? businessId,
        string? applicationUserRole,
        PaginationFilter paginationFilter
    )
    {
        var query =
            from user in _context.Users.Include(u => u.OwnedBusiness).Include(u => u.EmployerBusiness)
            join userRole in _context.UserRoles on user.Id equals userRole.UserId
            join role in _context.Roles on userRole.RoleId equals role.Id
            where
                (
                    businessId == null
                    || (user.OwnedBusiness != null && user.OwnedBusiness.Id == businessId)
                    || (user.EmployerBusiness != null && user.EmployerBusiness.Id == businessId)
                ) && (string.IsNullOrEmpty(applicationUserRole) || role.Name == applicationUserRole)
            orderby user.UserName
            select new { user, RoleName = role.Name };

        var paginatedUsers = await query
            .Skip((paginationFilter.Page - 1) * paginationFilter.ItemsPerPage)
            .Take(paginationFilter.ItemsPerPage)
            .ToListAsync();

        return paginatedUsers.Select(x => (x.user, x.RoleName)).ToList();
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<List<ApplicationUser>> GetManyByIdsAsync(List<int> userIds)
    {
        var distinctIds = userIds.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }

        return await _context.Users.Join(distinctIds, e => e.Id, id => id, (e, _) => e).ToListAsync();
    }
}
