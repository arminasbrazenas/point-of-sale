using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Extensions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Utilities;
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
            PriceTaxExcluded = modifier.Price.ToRoundedPrice(),
            Price = modifier.Price.ToRoundedPrice(),
            Stock = modifier.Stock,
            BusinessId = modifier.BusinessId,
        };
    }

    public ModifierDTO MapToModifierDTO(Modifier modifier, IEnumerable<Tax> taxes)
    {
        var grossPrice = modifier.Price.ToRoundedPrice();
        var netPrice = grossPrice;
        foreach (var tax in taxes)
        {
            netPrice += tax.GetAmountToApply(netPrice);
        }

        return new ModifierDTO
        {
            Id = modifier.Id,
            Name = modifier.Name,
            PriceTaxExcluded = grossPrice,
            Price = netPrice,
            Stock = modifier.Stock,
            BusinessId = modifier.BusinessId,
        };
    }

    public PagedResponseDTO<ModifierDTO> MapToPagedModifierDTO(
        List<Modifier> modifiers,
        PaginationFilter paginationFilter,
        int totalCount
    )
    {
        return new PagedResponseDTO<ModifierDTO>
        {
            Page = paginationFilter.Page,
            ItemsPerPage = paginationFilter.ItemsPerPage,
            TotalItems = totalCount,
            Items = modifiers.Select(MapToModifierDTO).ToList(),
        };
    }
}
