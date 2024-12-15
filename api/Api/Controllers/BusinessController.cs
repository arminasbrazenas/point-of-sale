using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.BusinessManagement.DTOs;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

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

    [HttpGet]
    [Route("{businessId:int}")]
    [Authorize(Roles = "Admin,BusinessOwner,Employee")]
    public async Task<ActionResult<BusinessDTO>> GetBusiness(int businessId)
    {
        var business = await _businessService.GetBusiness(businessId);
        return Ok(business);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BusinessDTO>> GetBusinesses([FromQuery] PaginationFilterDTO paginationFilterDTO)
    {
        var businesses = await _businessService.GetBusinesses(paginationFilterDTO);
        return Ok(businesses);
    }

    [HttpPatch]
    [Route("{businessId:int}")]
    [Authorize(Roles = "Admin,BusinessOwner")]
    public async Task<IActionResult> ModifyBusiness(int businessId, [FromBody] UpdateBusinessDTO updateBusinessDTO)
    {
        var business = await _businessService.UpdateBusiness(businessId, updateBusinessDTO);

        return Ok(business);
    }

    [HttpDelete]
    [Route("{businessId:int}")]
    [Authorize(Roles = "Admin,BusinessOwner")]
    public async Task<ActionResult<BusinessDTO>> DeleteBusiness(int businessId)
    {
        await _businessService.DeleteBusiness(businessId);
        return NoContent();
    }
}
