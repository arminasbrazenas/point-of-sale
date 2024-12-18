using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IModifierRepository : IRepositoryBase<Modifier, int>
{
    Task<bool> ExistsWithName(string name, int businessId);
    Task<List<Modifier>> GetWithFilter(
        PaginationFilter paginationFilter,
        int businessId,
        ModifierFilter? modifierFilter = null
    );
    Task<int> GetTotalCount(int businessId);
}
