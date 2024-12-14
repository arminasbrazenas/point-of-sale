namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateReservationAndContactInfoDTO
{
    public required CreateReservationDTO CreateReservationDTO { get; init; }
    public required CreateContactInfoDTO CreateContactInfoDTO { get; init; }
}