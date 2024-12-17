using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationMappingService
{
    ReservationDTO MapToReservationDTO(Reservation reservation);

    PagedResponseDTO<ReservationDTO> MapToPagedReservationDTO(
        List<Reservation> reservations,
        PaginationFilter paginationFilter,
        int totalCount
    );
}