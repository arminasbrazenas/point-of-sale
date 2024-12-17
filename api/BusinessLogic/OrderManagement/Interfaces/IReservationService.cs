using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.OrderManagement.Filters;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationService
{
    Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDTO);
    Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto);
    Task DeleteReservation(int reservationId);
    Task<ReservationDTO> CancelReservation(int reservationId);
    Task<ReservationDTO> GetReservation(int reservationId);
    Task<PagedResponseDTO<ReservationDTO>> GetReservations(
        PaginationFilterDTO paginationFilterDTO,
        int businessId,
        ReservationFilter? filter
    );
}
