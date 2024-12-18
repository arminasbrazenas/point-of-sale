using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.Shared.Filters;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationService
{
    Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDto, PaginationFilter paginationFilter);

    Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto, PaginationFilter paginationFilter);
    Task DeleteReservation(int reservationId);
    Task<ReservationDTO> CancelReservation(int reservationId);
    Task<ReservationDTO> GetReservation(int reservationId);
    Task<PagedResponseDTO<ReservationDTO>> GetReservations(
        PaginationFilterDTO paginationFilterDTO,
        int businessId,
        ReservationFilter? filter
    );
    Task CompleteReservation(int reservationId);
    Task MarkReservationInProgress(int reservationId);
    Task RevertInProgressReservation(int reservationId);
    Task SendUnsentNotifications();
}
