using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.Shared.DTOs;

namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface IModifierService
{
    Task<ModifierDTO> CreateModifier(CreateModifierDTO createModifierDTO);
    Task<PagedResponseDTO<ModifierDTO>> GetModifiers(PaginationFilterDTO paginationFilterDTO, int businessId);
    Task<ModifierDTO> GetModifier(int modifierId);
    Task<ModifierDTO> UpdateModifier(int modifierId, UpdateModifierDTO updateModifierDTO);
    Task DeleteModifier(int modifierId);
}
