using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ReservationMappingService : IReservationMappingService
{
    public ReservationDTO MapToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO
        {
            Id = reservation.Id,
            Date = reservation.Date,
            Status = reservation.Status,
            Customer = reservation.Customer,
            ServiceId = reservation.ServiceId,
            EmployeeId = reservation.EmployeeId,
            BusinessId = reservation.BusinessId,
        };
    }
    
    public PagedResponseDTO<ReservationDTO> MapToPagedReservationDTO(List<Reservation> reservations, PaginationFilter paginationFilter, int totalCount)
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