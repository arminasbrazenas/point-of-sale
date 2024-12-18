using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.Shared.Filters;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> CreateReservation(
        [FromBody] CreateReservationDTO createReservationDto,
        [FromQuery] PaginationFilter paginationFilter
    )
    {
        var reservation = await _reservationService.CreateReservation(createReservationDto, paginationFilter);
        return Ok(reservation);
    }

    [HttpPatch]
    [Route("{reservationId:int}")]
    public async Task<ActionResult<ReservationDTO>> UpdateReservation(
        [FromRoute] int reservationId,
        [FromBody] UpdateReservationDTO updateReservationDto,
        [FromQuery] PaginationFilter paginationFilter
    )
    {
        var reservation = await _reservationService.UpdateReservation(reservationId, updateReservationDto, paginationFilter);
        return Ok(reservation);
    }

    [HttpGet]
    public async Task<ActionResult<ReservationDTO>> GetReservations(
        [FromQuery] int businessId,
        [FromQuery] PaginationFilterDTO paginationFilterDTO,
        [FromQuery] ReservationFilter? filter = null
    )
    {
        var reservation = await _reservationService.GetReservations(paginationFilterDTO, businessId, filter);
        return Ok(reservation);
    }

    [HttpGet]
    [Route("{reservationId:int}")]
    public async Task<ActionResult<ReservationDTO>> GetReservation([FromRoute] int reservationId)
    {
        var reservation = await _reservationService.GetReservation(reservationId);
        return Ok(reservation);
    }

    [HttpPost]
    [Route("{reservationId:int}/cancel")]
    public async Task<ActionResult<ReservationDTO>> CancelReservation([FromRoute] int reservationId)
    {
        var reservation = await _reservationService.CancelReservation(reservationId);
        return Ok(reservation);
    }

    [HttpDelete]
    [Route("{reservationId:int}")]
    public async Task<ActionResult<ReservationDTO>> DeleteReservation([FromRoute] int reservationId)
    {
        await _reservationService.DeleteReservation(reservationId);
        return NoContent();
    }
}
