using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IModifierRepository : IRepositoryBase<Modifier, int>
{
    Task<List<Modifier>> GetWithFilter(
        PaginationFilter paginationFilter,
        ModifierFilter? modifierFilter = null);
}