using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/discounts")]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService
    
    [HttpPost]
    public async Task<ActionResult<DiscountDTO>> CreateDiscount([FromBody] CreateDiscountDTO createDiscountDTO)
    {
        
    }
}