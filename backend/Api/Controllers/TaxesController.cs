using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.Order.DTOs;
using PointOfSale.BusinessLogic.Order.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
}
