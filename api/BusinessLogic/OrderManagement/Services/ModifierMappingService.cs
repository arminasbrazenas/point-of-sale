using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ModifierMappingService : IModifierMappingService
{
    public ModifierDTO MapToModifierDTO(Modifier modifier)
    {
        return new ModifierDTO
        {
            Id = modifier.Id,
            Name = modifier.Name,
            Price = modifier.Price,
            Amount = modifier.Stock,
        };
    }

    public PagedResponseDTO<ModifierDTO> MapToPagedModifierDTO(
        List<Modifier> modifiers,
        PaginationFilter paginationFilter
    )
    {
        return new PagedResponseDTO<ModifierDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            Items = modifiers.Select(MapToModifierDTO).ToList(),
        };
    }
}
