using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/gift-cards")]
public class GiftCardsController : ControllerBase
{
    private readonly IGiftCardService _giftCardService;

    public GiftCardsController(IGiftCardService giftCardService)
    {
        _giftCardService = giftCardService;
    }

    [HttpPost]
    public async Task<ActionResult<GiftCardDTO>> CreateGiftCard([FromBody] CreateGiftCardDTO createGiftCardDTO)
    {
        var giftCardDTO = await _giftCardService.CreateGiftCard(createGiftCardDTO);
        return Ok(giftCardDTO);
    }

    [HttpGet]
    [Route("{giftCardId:int}")]
    public async Task<ActionResult<GiftCardDTO>> GetGiftCard([FromRoute] int giftCardId)
    {
        var giftCard = await _giftCardService.GetGiftCard(giftCardId);
        return Ok(giftCard);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<GiftCardDTO>>> GetGiftCards(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var giftCards = await _giftCardService.GetGiftCards(paginationFilterDTO);
        return Ok(giftCards);
    }

    [HttpPatch]
    [Route("{giftCardId:int}")]
    public async Task<ActionResult<GiftCardDTO>> UpdateGiftCard(
        [FromRoute] int giftCardId,
        [FromBody] UpdateGiftCardDTO updateGiftCardDTO
    )
    {
        var giftCard = await _giftCardService.UpdateGiftCard(giftCardId, updateGiftCardDTO);
        return Ok(giftCard);
    }

    [HttpDelete]
    [Route("{giftCardId:int}")]
    public async Task<IActionResult> DeleteGiftCard([FromRoute] int giftCardId)
    {
        await _giftCardService.DeleteGiftCard(giftCardId);
        return NoContent();
    }
}
