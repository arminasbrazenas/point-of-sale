using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.BusinessManagement.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    public UserService(UserManager<User> userManager){
        _userManager = userManager;
    }

    public async Task<User> GetUser(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user!;
    }

}