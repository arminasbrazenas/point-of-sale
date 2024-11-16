namespace PointOfSale.Api.DTOs;

public record DevelopmentErrorResponseDTO : ProductionErrorResponseDTO
{
    public required string? StackTrace { get; init; }
}
