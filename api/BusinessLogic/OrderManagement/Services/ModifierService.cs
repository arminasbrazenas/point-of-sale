using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ModifierService : IModifierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IModifierRepository _modifierRepository;
    private readonly IModifierMappingService _modifierMappingService;
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;

    public ModifierService(
        IUnitOfWork unitOfWork,
        IModifierRepository modifierRepository,
        IModifierMappingService modifierMappingService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService
    )
    {
        _unitOfWork = unitOfWork;
        _modifierRepository = modifierRepository;
        _modifierMappingService = modifierMappingService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
    }

    public async Task<ModifierDTO> CreateModifier(CreateModifierDTO createModifierDTO)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createModifierDTO.BusinessId);

        var modifier = new Modifier
        {
            Name = createModifierDTO.Name,
            Price = createModifierDTO.Price.ToRoundedPrice(),
            Stock = createModifierDTO.Stock,
            Products = [],
            BusinessId = createModifierDTO.BusinessId,
        };

        _modifierRepository.Add(modifier);
        await _unitOfWork.SaveChanges();

        return _modifierMappingService.MapToModifierDTO(modifier);
    }

    public async Task<PagedResponseDTO<ModifierDTO>> GetModifiers(
        PaginationFilterDTO paginationFilterDTO,
        int businessId
    )
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var modifiers = await _modifierRepository.GetWithFilter(paginationFilter, businessId);
        var totalCount = await _modifierRepository.GetTotalCount(businessId);
        return _modifierMappingService.MapToPagedModifierDTO(modifiers, paginationFilter, totalCount);
    }

    public async Task<ModifierDTO> GetModifier(int modifierId)
    {
        var modifier = await _modifierRepository.Get(modifierId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(modifier.BusinessId);

        return _modifierMappingService.MapToModifierDTO(modifier);
    }

    public async Task<ModifierDTO> UpdateModifier(int modifierId, UpdateModifierDTO updateModifierDTO)
    {
        var modifier = await _modifierRepository.Get(modifierId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(modifier.BusinessId);

        if (updateModifierDTO.Name is not null)
        {
            modifier.Name = updateModifierDTO.Name;
        }

        if (updateModifierDTO.Price.HasValue)
        {
            modifier.Price = updateModifierDTO.Price.Value.ToRoundedPrice();
        }

        if (updateModifierDTO.Stock.HasValue)
        {
            modifier.Stock = updateModifierDTO.Stock.Value;
        }

        _modifierRepository.Update(modifier);
        await _unitOfWork.SaveChanges();

        return _modifierMappingService.MapToModifierDTO(modifier);
    }

    public async Task DeleteModifier(int modifierId)
    {
        var modifier = await _modifierRepository.Get(modifierId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(modifier.BusinessId);

        await _modifierRepository.Delete(modifierId);
    }
}
