using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class DiscountRepository : RepositoryBase<Discount, int>, IDiscountRepository
{
    public DiscountRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Discount> GetWithProducts(int discountId)
    {
        var discount = await DbSet.Include(d => d.AppliesTo).FirstOrDefaultAsync(d => d.Id == discountId);

        if (discount is null)
        {
            throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(discountId));
        }

        return discount;
    }

    public async Task<List<Discount>> GetPagedWithProducts(PaginationFilter paginationFilter)
    {
        var query = DbSet.Include(d => d.AppliesTo).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new DiscountNotFoundErrorMessage(id);
    }
}
