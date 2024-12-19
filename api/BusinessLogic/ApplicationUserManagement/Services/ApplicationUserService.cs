using Microsoft.AspNetCore.Identity;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Exceptions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IApplicationUserValidationService _applicationUserValidationService;
    private readonly IApplicationUserMappingService _applicationUserMappingService;
    private readonly IApplicationUserAuthorizationService _applicationUserAuthorizationService;
    private readonly ICurrentApplicationUserAccessor _currentApplicationUserAccessor;
    private readonly IApplicationUserTokenService _tokenService;
    private readonly IContactInfoValidationService _contactInfoValidationService;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        IApplicationUserValidationService applicationUserValidationService,
        IApplicationUserMappingService applicationUserMappingService,
        IApplicationUserAuthorizationService applicationUserAuthorizationService,
        IApplicationUserRepository applicationUserRepository,
        ICurrentApplicationUserAccessor currentApplicationUserAccessor,
        IApplicationUserTokenService tokenService,
        IContactInfoValidationService contactInfoValidationService
    )
    {
        _userManager = userManager;
        _applicationUserValidationService = applicationUserValidationService;
        _applicationUserMappingService = applicationUserMappingService;
        _applicationUserAuthorizationService = applicationUserAuthorizationService;
        _applicationUserRepository = applicationUserRepository;
        _currentApplicationUserAccessor = currentApplicationUserAccessor;
        _tokenService = tokenService;
        _contactInfoValidationService = contactInfoValidationService;
    }

    public async Task<TokensDTO> AuthenticateApplicationUser(LoginApplicationUserDTO dto)
    {
        if (
            await _userManager.FindByEmailAsync(dto.Email) is { } user
            && await _userManager.CheckPasswordAsync(user, dto.Password)
            && user.IsActive
        )
        {
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var accessToken = _tokenService.GetApplicationUserAccessToken(user, userRole!);
            var refreshToken = await _tokenService.GetApplicationUserRefreshToken(user, userRole!);

            return _applicationUserMappingService.MapTokensDTO(accessToken, refreshToken);
        }

        throw new ApplicationUserAuthenticationException(new InvalidApplicationUserCredentialsErrorMessage());
    }

    public async Task<ApplicationUserDTO> GetApplicationUserByEmail(string email)
    {
        if (await _applicationUserRepository.GetUserByEmailWithBusinessAsync(email) is { } user && user.IsActive)
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
        await _applicationUserAuthorizationService.AuthorizeApplicationUserAction(id);
        if (await _applicationUserRepository.GetUserByIdWithBusinessAsync(id) is { } user && user.IsActive)
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
            PhoneNumber = dto.PhoneNumber,
            EmployerBusinessId = dto.BusinessId,
        };

        var result = await _userManager.CreateAsync(applicationUser, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(applicationUser, dto.Role);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(new FailedActionOnApplicationUserErrorMessage(errors));
        }

        return _applicationUserMappingService.MapApplicationUserDTO(applicationUser, dto.Role);
    }

    public async Task<PagedResponseDTO<ApplicationUserDTO>> GetApplicationUsers(
        int? businessId,
        string? role,
        PaginationFilterDTO paginationFilterDTO
    )
    {
        await _applicationUserAuthorizationService.AuthorizeGetApplicationUsersAction(businessId);

        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);

        var users = await _applicationUserRepository.GetAllUsersWithBusinessAsync(businessId, role, paginationFilter);

        var totalCount = await _applicationUserRepository.GetTotalCountAsync(businessId);

        var userDtos = _applicationUserMappingService.MapPagedApplicationUserDTOs(users, paginationFilter, totalCount);

        return userDtos;
    }

    public async Task<ApplicationUserDTO> GetCurrentApplicationUser()
    {
        var applicationUserId = _currentApplicationUserAccessor.GetApplicationUserId();
        var role = _currentApplicationUserAccessor.GetApplicationUserRole();
        var applicationUser = await _applicationUserRepository.GetUserByIdWithBusinessAsync(applicationUserId);
        return _applicationUserMappingService.MapApplicationUserDTO(applicationUser!, role);
    }

    public async Task<ApplicationUserDTO> UpdateApplicationUser(
        int applicationUserId,
        UpdateApplicationUserDTO updateApplicationUserDTO
    )
    {
        await _applicationUserAuthorizationService.AuthorizeApplicationUserAction(applicationUserId);
        var user = await _userManager.FindByIdAsync(applicationUserId.ToString());

        if (user is null || !user.IsActive)
        {
            throw new PointOfSaleException(new ApplicationUserNotFoundErrorMessage(applicationUserId));
        }

        if (updateApplicationUserDTO.FirstName is not null)
        {
            user.FirstName = updateApplicationUserDTO.FirstName;
        }

        if (updateApplicationUserDTO.LastName is not null)
        {
            user.LastName = updateApplicationUserDTO.LastName;
        }

        if (updateApplicationUserDTO.Email is not null)
        {
            _contactInfoValidationService.ValidateEmail(updateApplicationUserDTO.Email);
            user.Email = updateApplicationUserDTO.Email;
        }

        if (updateApplicationUserDTO.PhoneNumber is not null)
        {
            _contactInfoValidationService.ValidatePhoneNumber(updateApplicationUserDTO.PhoneNumber);
            user.PhoneNumber = updateApplicationUserDTO.PhoneNumber;
        }

        if (updateApplicationUserDTO.Password is not null)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, updateApplicationUserDTO.Password);
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(new FailedActionOnApplicationUserErrorMessage(errors));
        }

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        return _applicationUserMappingService.MapApplicationUserDTO(user, role!);
    }

    public async Task<TokensDTO> RefreshApplicationUserTokens(string? refreshToken)
    {
        var user = await _tokenService.UseApplicationUserRefreshToken(refreshToken);
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        var accessToken = _tokenService.GetApplicationUserAccessToken(user, role!);
        var newRefreshToken = await _tokenService.GetApplicationUserRefreshToken(user, role!);
        return new TokensDTO(accessToken, newRefreshToken);
    }

    public async Task CreateAdminUser(RegisterApplicationUserDTO dto)
    {
        if (await _applicationUserRepository.DoesAdminExist())
        {
            return;
        }
        await _applicationUserValidationService.ValidateRegisterApplicationUserDTO(dto);

        var applicationUser = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            EmployerBusinessId = dto.BusinessId,
        };

        var result = await _userManager.CreateAsync(applicationUser, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(applicationUser, "Admin");
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(new FailedActionOnApplicationUserErrorMessage(errors));
        }
    }

    public async Task DeleteApplicationUser(int applicationUserId)
    {
        await _applicationUserAuthorizationService.AuthorizeApplicationUserAction(applicationUserId);
        var user = await _userManager.FindByIdAsync(applicationUserId.ToString());

        if (user is null)
        {
            throw new PointOfSaleException(new ApplicationUserNotFoundErrorMessage(applicationUserId));
        }
        else
        {
            user.Email = $"ANONYMIZED{user.Id}@ANONYMIZED.COM";
            user.PhoneNumber = "ANONYMIZED";
            user.FirstName = "ANONYMIZED";
            user.LastName = "ANONYMIZED";
            user.UserName = "ANONYMIZED";
            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ValidationException(new FailedActionOnApplicationUserErrorMessage(errors));
            }
        }
    }
}
