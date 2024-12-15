using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ModifierRepository : RepositoryBase<Modifier, int>, IModifierRepository
{
    public ModifierRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Modifier>> GetWithFilter(
        PaginationFilter paginationFilter,
        int businessId,
        ModifierFilter? modifierFilter = null
    )
    {
        var query = DbSet.Where(m => m.BusinessId == businessId).OrderBy(m => m.CreatedAt).AsQueryable();

        if (modifierFilter?.CompatibleWithProductById is not null)
        {
            query = query.Where(m => m.Products.Any(p => p.Id == modifierFilter.CompatibleWithProductById));
        }

        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ModifierNotFoundErrorMessage(id);
    }

    public override async Task<int> GetTotalCount(int? businessId = null){
        if (businessId.HasValue)
        {
            return await DbSet.Where(m => m.BusinessId == businessId).CountAsync();
        }

        return await base.GetTotalCount(businessId);
    }
}
