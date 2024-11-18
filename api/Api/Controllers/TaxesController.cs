using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/taxes")]
public class TaxesController : ControllerBase
{
    private readonly ITaxService _taxService;

    public TaxesController(ITaxService taxService)
    {
        _taxService = taxService;
    }

    [HttpPost]
    public async Task<ActionResult<TaxDTO>> CreateTax([FromBody] CreateTaxDTO createTaxDTO)
    {
        var tax = await _taxService.CreateTax(createTaxDTO);
        return Ok(tax);
    }

    [HttpPatch]
    [Route("{taxId:int}")]
    public async Task<ActionResult<TaxDTO>> UpdateTax([FromRoute] int taxId, [FromBody] UpdateTaxDTO updateTaxDTO)
    {
        var tax = await _taxService.UpdateTax(taxId, updateTaxDTO);
        return Ok(tax);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<TaxDTO>>> GetTaxes(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var taxes = await _taxService.GetTaxes(paginationFilterDTO);
        return Ok(taxes);
    }

    [HttpGet]
    [Route("{taxId:int}")]
    public async Task<ActionResult<TaxDTO>> GetTax([FromRoute] int taxId)
    {
        var tax = await _taxService.GetTax(taxId);
        return Ok(tax);
    }

    [HttpDelete]
    [Route("{taxId:int}")]
    public async Task<IActionResult> DeleteTax([FromRoute] int taxId)
    {
        await _taxService.DeleteTax(taxId);
        return NoContent();
    }
}
