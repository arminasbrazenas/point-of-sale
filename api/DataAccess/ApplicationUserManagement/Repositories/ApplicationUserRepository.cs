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
        return await _context.Users.Include(u => u.Business).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ApplicationUser?> GetUserByEmailWithBusinessAsync(string email)
    {
        return await _context.Users.Include(u => u.Business).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<(ApplicationUser User, string Role)>> GetAllUsersWithBusinessAsync(
        int? businessId,
        PaginationFilter paginationFilter
    )
    {
        var query =
            from user in _context.Users.Include(u => u.Business)
            join userRole in _context.UserRoles on user.Id equals userRole.UserId
            join role in _context.Roles on userRole.RoleId equals role.Id
            where businessId == null || user.Business != null && user.Business.Id == businessId
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
}
