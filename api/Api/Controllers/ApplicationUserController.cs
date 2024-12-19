using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly CookieOptions _cookieOptions;
    private readonly IConfiguration _configuration;

    public ApplicationUserController(IApplicationUserService applicationUserService, IConfiguration configuration)
    {
        _applicationUserService = applicationUserService;
        _configuration = configuration;
        _cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:CookieExpirationTime"]!)),
        };
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterApplicationUser([FromBody] RegisterApplicationUserDTO request)
    {
        var user = await _applicationUserService.CreateApplicationUser(request);

        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,BusinessOwner,Employee")]
    [Route("")]
    public async Task<IActionResult> GetApplicationUsers(
        [FromQuery] int? businessId,
        [FromQuery] string? role,
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var pagedUsers = await _applicationUserService.GetApplicationUsers(businessId, role, paginationFilterDTO);
        return Ok(pagedUsers);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,BusinessOwner,Employee")]
    [Route("currentUser")]
    public async Task<IActionResult> GetCurrentApplicationUser()
    {
        var user = await _applicationUserService.GetCurrentApplicationUser();
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,BusinessOwner,Employee")]
    [Route("{userId:int}")]
    public async Task<IActionResult> GetApplicationUser(int userId)
    {
        var user = await _applicationUserService.GetApplicationUserById(userId);
        return Ok(user);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin,BusinessOwner")]
    [Route("{userId:int}")]
    public async Task<IActionResult> UpdateApplicationUser(
        int userId,
        [FromBody] UpdateApplicationUserDTO updateApplicationUserDTO
    )
    {
        var user = await _applicationUserService.UpdateApplicationUser(userId, updateApplicationUserDTO);
        return Ok(user);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin,BusinessOwner")]
    [Route("{userId:int}")]
    public async Task<IActionResult> DeleteApplicationUser(int userId)
    {
        await _applicationUserService.DeleteApplicationUser(userId);
        return NoContent();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginApplicationUser([FromBody] LoginApplicationUserDTO request)
    {
        var tokens = await _applicationUserService.AuthenticateApplicationUser(request);

        Response.Cookies.Append("AccessToken", tokens.AccessToken, _cookieOptions);
        Response.Cookies.Append("RefreshToken", tokens.RefreshToken, _cookieOptions);

        var user = await _applicationUserService.GetApplicationUserByEmail(request.Email);

        return Ok(user);
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult LogoutApplicationUser()
    {
        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");

        return Ok();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshAccessToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];

        var newTokens = await _applicationUserService.RefreshApplicationUserTokens(refreshToken);

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");

        Response.Cookies.Append("AccessToken", newTokens.AccessToken);
        Response.Cookies.Append("RefreshToken", newTokens.RefreshToken);

        return Ok();
    }
}
