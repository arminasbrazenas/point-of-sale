using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
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

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersWithBusinessAsync()
    {
        return await _context.Users.Include(u => u.Business).ToListAsync();
    }
}
