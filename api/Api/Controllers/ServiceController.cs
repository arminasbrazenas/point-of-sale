using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

namespace PointOfSale.Api.Controllers;


[ApiController]
[Route("service")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDTO>> CreateService([FromBody] CreateServiceDTO createServiceDto)
    {
        var service = await _serviceService.CreateService(createServiceDto);
        return Ok(service);
    }

    [HttpPatch]
    [Route("{serviceId:int}")]
    public async Task<ActionResult<ServiceDTO>> UpdateService([FromRoute]int serviceId, [FromBody] UpdateServiceDTO updateServiceDto)
    {
        var service = await _serviceService.UpdateService(serviceId, updateServiceDto);
        return Ok(service);
    }
    
    [HttpGet]
    [Route("{serviceId:int}")]
    public async Task<ActionResult<ServiceDTO>> GetService([FromRoute]int serviceId)
    {
        var service = await _serviceService.GetService(serviceId);
        return Ok(service);
    }
    
    [HttpDelete]
    [Route("{serviceId:int}")]
    public async Task<ActionResult<ServiceDTO>> DeleteService([FromRoute]int serviceId)
    {
        await _serviceService.DeleteService(serviceId);
        return NoContent();
    }
}