using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ReservationMappingService : IReservationMappingService
{
    private readonly IServiceMappingService _serviceMappingService;

    public ReservationMappingService(IServiceMappingService serviceMappingService)
    {
        _serviceMappingService = serviceMappingService;
    }

    public ReservationDTO MapToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO
        {
            Id = reservation.Id,
            Description = reservation.Name,
            Date = reservation.Date,
            Status = reservation.Status,
            Customer = reservation.Customer,
            Employee = _serviceMappingService.MapToServiceEmployeeDTO(reservation.Employee),
            ServiceId = reservation.ServiceId,
            Price = reservation.Price,
            BusinessId = reservation.BusinessId,
            BookedAt = reservation.CreatedAt,
        };
    }

    public PagedResponseDTO<ReservationDTO> MapToPagedReservationDTO(
        List<Reservation> reservations,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<ReservationDTO>
        {
            Items = reservations.Select(MapToReservationDTO).ToList(),
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Page = paginationFilter.Page,
        };
    }
}
