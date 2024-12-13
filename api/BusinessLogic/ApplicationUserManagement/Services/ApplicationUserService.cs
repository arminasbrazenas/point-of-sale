using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IApplicationUserValidationService _applicationUserValidationService;
    private readonly IApplicationUserMappingService _applicationUserMappingService;
    private readonly IApplicationUserAuthorizationService _applicationUserAuthorizationService;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        IApplicationUserValidationService applicationUserValidationService,
        IApplicationUserMappingService applicationUserMappingService,
        IApplicationUserAuthorizationService applicationUserAuthorizationService,
        IApplicationUserRepository applicationUserRepository
    )
    {
        _userManager = userManager;
        _applicationUserValidationService = applicationUserValidationService;
        _applicationUserMappingService = applicationUserMappingService;
        _applicationUserAuthorizationService = applicationUserAuthorizationService;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<TokensDTO> AuthenticateApplicationUser(LoginApplicationUserDTO dto)
    {
        if (
            await _userManager.FindByEmailAsync(dto.Email) is { } user
            && await _userManager.CheckPasswordAsync(user, dto.Password)
        )
        {
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var accessToken = _applicationUserAuthorizationService.GetApplicationUserAccessToken(user, userRole!);
            var refreshToken = await _applicationUserAuthorizationService.GetApplicationUserRefreshToken(
                user,
                userRole!
            );

            return _applicationUserMappingService.MapTokensDTO(accessToken, refreshToken);
        }

        throw new ApplicationUserAuthenticationException(new InvalidApplicationUserCredentialsErrorMessage());
    }

    public async Task<ApplicationUserDTO> GetApplicationUserByEmail(string email)
    {
        if (await _applicationUserRepository.GetUserByEmailWithBusinessAsync(email) is { } user)
        {
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return _applicationUserMappingService.MapApplicationUserDTO(user, role!);
        }
        else
        {
            throw new ApplicationUserAuthenticationException(new InvalidApplicationUserCredentialsErrorMessage());
        }
    }

    public async Task<ApplicationUserDTO> GetApplicationUserById(int id)
    {
        if (await _applicationUserRepository.GetUserByIdWithBusinessAsync(id) is { } user)
        {
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return _applicationUserMappingService.MapApplicationUserDTO(user, role!);
        }
        else
        {
            throw new ApplicationUserAuthenticationException(new InvalidApplicationUserCredentialsErrorMessage());
        }
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
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"User creation failed: {error.Code} - {error.Description}");
            }
        }

        return _applicationUserMappingService.MapApplicationUserDTO(applicationUser, dto.Role);
    }

    public async Task<List<ApplicationUserDTO>> GetApplicationUsers()
    {
        var users = await _applicationUserRepository.GetAllUsersWithBusinessAsync();

        var userDtos = await Task.WhenAll(
            users.Select(async user =>
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                return _applicationUserMappingService.MapApplicationUserDTO(user, role!);
            })
        );

        return userDtos.ToList();
    }
}
