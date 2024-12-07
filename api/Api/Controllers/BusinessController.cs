using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/businesses")]
public class BusinessesController : ControllerBase
{
    private readonly IBusinessService _businessService;

    public BusinessesController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,BusinessOwner")]
    public async Task<ActionResult<BusinessDTO>> CreateBusiness([FromBody] CreateBusinessDTO createBusinessDTO)
    {
        var business = await _businessService.CreateBusiness(createBusinessDTO);
        return Ok(business);
    }
}
