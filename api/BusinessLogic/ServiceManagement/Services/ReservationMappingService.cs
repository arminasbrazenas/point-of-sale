using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ReservationMappingService : IReservationMappingService
{
    public ReservationDTO MapToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO
        {
            Id = reservation.Id,
            DateStart = reservation.DateStart,
            DateEnd = reservation.DateEnd,
            Status = reservation.Status,
            ServiceResourceId = reservation.ServiceResourceId,
            ContactInfoId = reservation.ContactInfoId,
            ServiceId = reservation.ServiceId,
            EmployeeId = reservation.EmployeeId,
            CreatedAt = reservation.CreatedAt,
            LastUpdated = reservation.LastUpdated,
        };
    }
}