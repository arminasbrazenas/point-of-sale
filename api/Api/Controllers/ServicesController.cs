using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/services")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
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
    public async Task<ActionResult<ServiceDTO>> UpdateService(
        [FromRoute] int serviceId,
        [FromBody] UpdateServiceDTO updateServiceDto
    )
    {
        var service = await _serviceService.UpdateService(serviceId, updateServiceDto);
        return Ok(service);
    }

    [HttpGet]
    [Route("{serviceId:int}")]
    public async Task<ActionResult<ServiceDTO>> GetService([FromRoute] int serviceId)
    {
        var service = await _serviceService.GetService(serviceId);
        return Ok(service);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<ServiceDTO>>> GetServices(
        [FromQuery] int businessId,
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var service = await _serviceService.GetServices(paginationFilterDTO, businessId);
        return Ok(service);
    }

    [HttpDelete]
    [Route("{serviceId:int}")]
    public async Task<ActionResult<ServiceDTO>> DeleteService([FromRoute] int serviceId)
    {
        await _serviceService.DeleteService(serviceId);
        return NoContent();
    }
}
