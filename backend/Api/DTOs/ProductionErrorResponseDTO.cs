namespace PointOfSale.Api.DTOs;

public record ProductionErrorResponseDTO
{
    public required string ErrorMessage { get; init; }
}
