using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IModifierMappingService
{
    ModifierDTO MapToModifierDTO(Modifier modifier);
    PagedResponseDTO<ModifierDTO> MapToPagedModifierDTO(List<Modifier> modifiers, PaginationFilter paginationFilter);
}