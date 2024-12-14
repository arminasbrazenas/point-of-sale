using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Authorize(Roles = "BusinessOwner,Employee")]
[Route("v1/modifiers")]
public class ModifiersController : ControllerBase
{
    private readonly IModifierService _modifierService;

    public ModifiersController(IModifierService modifierService)
    {
        _modifierService = modifierService;
    }

    [HttpPost]
    public async Task<ActionResult<ModifierDTO>> CreateModifier([FromBody] CreateModifierDTO createModifierDTO)
    {
        var modifier = await _modifierService.CreateModifier(createModifierDTO);
        return Ok(modifier);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDTO<ModifierDTO>>> GetModifiers(
        [FromQuery] PaginationFilterDTO paginationFilterDTO
    )
    {
        var modifiers = await _modifierService.GetModifiers(paginationFilterDTO);
        return Ok(modifiers);
    }

    [HttpGet]
    [Route("{modifierId:int}")]
    public async Task<ActionResult<ModifierDTO>> GetModifier([FromRoute] int modifierId)
    {
        var modifier = await _modifierService.GetModifier(modifierId);
        return Ok(modifier);
    }

    [HttpPatch]
    [Route("{modifierId:int}")]
    public async Task<ActionResult<ModifierDTO>> UpdateModifier(
        [FromRoute] int modifierId,
        [FromBody] UpdateModifierDTO updateModifierDTO
    )
    {
        var modifier = await _modifierService.UpdateModifier(modifierId, updateModifierDTO);
        return Ok(modifier);
    }

    [HttpDelete]
    [Route("{modifierId:int}")]
    public async Task<IActionResult> DeleteModifier([FromRoute] int modifierId)
    {
        await _modifierService.DeleteModifier(modifierId);
        return NoContent();
    }
}
