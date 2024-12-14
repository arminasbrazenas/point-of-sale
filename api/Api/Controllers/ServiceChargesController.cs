using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Authorize(Roles = "BusinessOwner,Employee")]
[Route("v1/service-charges")]
public class ServiceChargesController : ControllerBase
{
    private readonly IServiceChargeService _serviceChargeService;

    public ServiceChargesController(IServiceChargeService serviceChargeService)
    {
        _serviceChargeService = serviceChargeService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceChargeDTO>> CreateServiceCharge(
        [FromBody] CreateServiceChargeDTO createServiceChargeDTO
    )
    {
        var serviceCharge = await _serviceChargeService.CreateServiceCharge(createServiceChargeDTO);
        return Ok(serviceCharge);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<ServiceChargeDTO>>> GetServiceCharges(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var serviceCharges = await _serviceChargeService.GetServiceCharges(paginationFilterDTO);
        return Ok(serviceCharges);
    }

    [HttpGet]
    [Route("{serviceChargeId:int}")]
    public async Task<ActionResult<ServiceChargeDTO>> GetServiceCharge([FromRoute] int serviceChargeId)
    {
        var serviceCharge = await _serviceChargeService.GetServiceCharge(serviceChargeId);
        return Ok(serviceCharge);
    }

    [HttpPatch]
    [Route("{serviceChargeId:int}")]
    public async Task<ActionResult<ServiceChargeDTO>> UpdateServiceCharge(
        [FromRoute] int serviceChargeId,
        [FromBody] UpdateServiceChargeDTO updateServiceChargeDTO
    )
    {
        var serviceCharge = await _serviceChargeService.UpdateServiceCharge(serviceChargeId, updateServiceChargeDTO);
        return Ok(serviceCharge);
    }

    [HttpDelete]
    [Route("{serviceChargeId:int}")]
    public async Task<IActionResult> DeleteServiceCharge([FromRoute] int serviceChargeId)
    {
        await _serviceChargeService.DeleteServiceCharge(serviceChargeId);
        return NoContent();
    }
}
