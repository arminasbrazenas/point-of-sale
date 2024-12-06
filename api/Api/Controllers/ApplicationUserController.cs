using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/users")]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;

    public ApplicationUserController(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterApplicationUser([FromBody] RegisterApplicationUserDTO request)
    {
        var user = await _applicationUserService.CreateApplicationUser(request);

        return Ok(user);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetApplicationUsers() {
        var users = await _applicationUserService.GetApplicationUsers();
        return Ok(users);
     }
}
