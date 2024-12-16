using PointOfSale.BusinessLogic.OrderManagement.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IReservationService
{
    Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDTO);
}