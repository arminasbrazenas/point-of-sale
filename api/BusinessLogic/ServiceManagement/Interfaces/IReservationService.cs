using PointOfSale.BusinessLogic.ServiceManagement.DTOs;

namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IReservationService
{
    public Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDto, CreateContactInfoDTO createContactInfoDto);
    public Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto);
    public Task DeleteReservation(int reservationId);
    public Task<ReservationDTO> CancelReservation(int reservationId);
    public Task<ReservationDTO> GetReservation(int reservationId);
}