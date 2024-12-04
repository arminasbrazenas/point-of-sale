using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetUser(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user!;
    }
}
