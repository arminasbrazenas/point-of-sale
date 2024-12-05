using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/discounts")]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpPost]
    public async Task<ActionResult<DiscountDTO>> CreateDiscount([FromBody] CreateDiscountDTO createDiscountDTO)
    {
        var discount = await _discountService.CreateDiscount(createDiscountDTO);
        return Ok(discount);
    }

    [HttpGet]
    [Route("{discountId:int}")]
    public async Task<ActionResult<DiscountDTO>> GetDiscount([FromRoute] int discountId)
    {
        var discount = await _discountService.GetDiscount(discountId);
        return Ok(discount);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<DiscountDTO>>> GetDiscounts(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var pagedDiscounts = await _discountService.GetDiscounts(paginationFilterDTO);
        return Ok(pagedDiscounts);
    }

    [HttpPatch]
    [Route("{discountId:int}")]
    public async Task<ActionResult<DiscountDTO>> UpdateDiscount(
        [FromRoute] int discountId,
        [FromBody] UpdateDiscountDTO updateDiscountDTO
    )
    {
        var discount = await _discountService.UpdateDiscount(discountId, updateDiscountDTO);
        return Ok(discount);
    }

    [HttpDelete]
    [Route("{discountId:int}")]
    public async Task<IActionResult> DeleteDiscount([FromRoute] int discountId)
    {
        await _discountService.DeleteDiscount(discountId);
        return NoContent();
    }
}
