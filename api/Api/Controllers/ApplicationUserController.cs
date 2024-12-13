using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

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
            SameSite = SameSiteMode.Lax,
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
    [Authorize(Roles = "Admin,BusinessOwner")]
    [Route("")]
    public async Task<IActionResult> GetApplicationUsers()
    {
        var users = await _applicationUserService.GetApplicationUsers();
        return Ok(users);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,BusinessOwner")]
    [Route("currentUser")]
    public async Task<IActionResult> GetCurrentApplicationUser()
    {
        var user =await  _applicationUserService.GetCurrentApplicationUser();
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,BusinessOwner")]
    [Route("{userId:int}")]
    public async Task<IActionResult> GetApplicationUsers(int userId)
    {
        var user = await _applicationUserService.GetApplicationUserById(userId);
        return Ok(user);
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
}
