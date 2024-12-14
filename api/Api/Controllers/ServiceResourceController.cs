using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

namespace PointOfSale.Api.Controllers;


[ApiController]
[Route("serviceResources")]
public class ServiceResourceController : ControllerBase
{
    private readonly IServiceResourceService _serviceResourceService;

    public ServiceResourceController(IServiceResourceService serviceResourceService)
    {
        _serviceResourceService = serviceResourceService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResourceDTO>> CreateServiceResource([FromBody] CreateServiceResourceDTO createServiceResourceDto)
    {
        var serviceResource = await _serviceResourceService.CreateServiceResource(createServiceResourceDto);
        return Ok(serviceResource);
    }

    [HttpPatch]
    [Route("{serviceResourceId:int}")]
    public async Task<ActionResult<ServiceResourceDTO>> UpdateServiceResource([FromRoute]int serviceResourceId, [FromBody] UpdateServiceResourceDTO updateServiceResourceDto)
    {
        var serviceResource = await _serviceResourceService.UpdateServiceResource(serviceResourceId, updateServiceResourceDto);
        return Ok(serviceResource);
    }
    
    [HttpGet]
    [Route("{serviceResourceId:int}")]
    public async Task<ActionResult<ServiceResourceDTO>> GetServiceResource([FromRoute]int serviceResourceId)
    {
        var serviceResource = await _serviceResourceService.GetServiceResource(serviceResourceId);
        return Ok(serviceResource);
    }
    
    [HttpDelete]
    [Route("{serviceResourceId:int}")]
    public async Task<ActionResult<ServiceResourceDTO>> DeleteServiceResource([FromRoute]int serviceResourceId)
    {
        await _serviceResourceService.DeleteServiceResource(serviceResourceId);
        return NoContent();
    }
}