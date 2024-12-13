using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Interfaces;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> GetUserByIdWithBusinessAsync(int id);
    Task<ApplicationUser?> GetUserByEmailWithBusinessAsync(string email);
    Task<IEnumerable<ApplicationUser>> GetAllUsersWithBusinessAsync();
}
