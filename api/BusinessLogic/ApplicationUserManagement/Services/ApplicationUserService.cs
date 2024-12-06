using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserValidationService _applicationUserValidationService;
    private readonly IApplicationUserMappingService _applicationUserMappingService;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        IApplicationUserValidationService applicationUserValidationService,
        IApplicationUserMappingService applicationUserMappingService
    )
    {
        _userManager = userManager;
        _applicationUserValidationService = applicationUserValidationService;
        _applicationUserMappingService = applicationUserMappingService;
    }

    public async Task<ApplicationUserDTO> CreateApplicationUser(RegisterApplicationUserDTO dto)
    {
        _applicationUserValidationService.ValidateApplicationUserRole(dto.Role);
        await _applicationUserValidationService.ValidateRegisterApplicationUserDTO(dto);

        var applicationUser = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };

        var result = await _userManager.CreateAsync(applicationUser, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(applicationUser, dto.Role);
        }

        else{
            foreach (var error in result.Errors)
    {
        Console.WriteLine($"User creation failed: {error.Code} - {error.Description}");
    }
        }

        return _applicationUserMappingService.MapApplicationUserDTO(applicationUser, dto.Role);
    }

    public async Task<List<ApplicationUserDTO>> GetApplicationUsers()
    {
        var users = _userManager.Users.ToList();

        var userDtos = await Task.WhenAll(
            users.Select(async user =>
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                return _applicationUserMappingService.MapApplicationUserDTO(user, role!);
            })
        );

        return userDtos.ToList();
    }
}
