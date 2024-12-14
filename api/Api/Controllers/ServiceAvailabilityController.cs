using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("ServiceAvailability")]
public class ServiceAvailabilityController : ControllerBase
{
    private readonly IServiceAvailabilityService _serviceAvailabilityService;

    public ServiceAvailabilityController(IServiceAvailabilityService serviceAvailabilityService)
    {
        _serviceAvailabilityService = serviceAvailabilityService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceAvailabilityDTO>> CreateServiceAvailability([FromBody] CreateServiceAvailabilityDTO createServiceAvailabilityDto)
    {
        var serviceAvailability = await _serviceAvailabilityService.CreateServiceAvailability(createServiceAvailabilityDto);
        return Ok(serviceAvailability);
    }

    [HttpPatch]
    [Route("{serviceAvailabilityId:int}")]
    public async Task<ActionResult<ServiceAvailabilityDTO>> UpdateServiceAvailability([FromRoute]int serviceAvailabilityId, [FromBody] UpdateServiceAvailabilityDTO updateServiceAvailabilityDto)
    {
        var serviceAvailability = await _serviceAvailabilityService.UpdateServiceAvailability(serviceAvailabilityId, updateServiceAvailabilityDto);
        return Ok(serviceAvailability);
    }
    
    [HttpGet]
    [Route("{serviceAvailabilityId:int}")]
    public async Task<ActionResult<ServiceAvailabilityDTO>> GetServiceAvailability([FromRoute]int serviceAvailabilityId)
    {
        var serviceAvailability = await _serviceAvailabilityService.GetServiceAvailability(serviceAvailabilityId);
        return Ok(serviceAvailability);
    }
    
    [HttpDelete]
    [Route("{serviceAvailabilityId:int}")]
    public async Task<ActionResult<ServiceAvailabilityDTO>> DeleteServiceAvailability([FromRoute]int serviceAvailabilityId)
    {
        await _serviceAvailabilityService.DeleteServiceAvailability(serviceAvailabilityId);
        return NoContent();
    }
}