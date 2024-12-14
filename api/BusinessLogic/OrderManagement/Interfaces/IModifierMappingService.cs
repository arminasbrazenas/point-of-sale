using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IModifierMappingService
{
    ModifierDTO MapToModifierDTO(Modifier modifier);
    ModifierDTO MapToModifierDTO(Modifier modifier, IEnumerable<Tax> taxes);
    PagedResponseDTO<ModifierDTO> MapToPagedModifierDTO(
        List<Modifier> modifiers,
        PaginationFilter paginationFilter,
        int totalCount
    );
}
