using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

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
