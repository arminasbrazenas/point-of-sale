namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ServiceDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int DurationInMinutes { get; init; }
    public required List<ServiceEmployeeDTO> ProvidedByEmployees { get; set; }
}

public record ServiceEmployeeDTO
{
    public required int Id { get; init; }
    public required string FullName { get; init; }
}
