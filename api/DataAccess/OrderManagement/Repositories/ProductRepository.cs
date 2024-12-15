using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Product> GetWithRelatedData(int productId)
    {
        var product = await DbSet
            .Include(p => p.Taxes)
            .Include(p => p.Modifiers)
            .Include(p => p.Discounts.Where(d => d.ValidUntil >= DateTimeOffset.UtcNow))
            .Where(p => p.Id == productId)
            .FirstOrDefaultAsync();

        return product ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
    }

    public async Task<List<Product>> GetManyWithRelatedData(IEnumerable<int> productIds)
    {
        var distinctIds = productIds.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }

        var products = await DbSet
            .Include(p => p.Taxes)
            .Include(p => p.Modifiers)
            .Include(p => p.Discounts.Where(d => d.ValidUntil >= DateTimeOffset.UtcNow))
            .Join(distinctIds, e => e.Id, id => id, (e, _) => e)
            .ToListAsync();

        return products;
    }

    public async Task<Product> GetWithModifiers(int productId)
    {
        var product = await DbSet.Include(p => p.Modifiers).Where(p => p.Id == productId).FirstOrDefaultAsync();

        return product ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
    }

    public Task<Product?> GetByNameOptional(string name)
    {
        return DbSet.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<List<Product>> GetPaged(PaginationFilter paginationFilter, int businessId)
    {
        var query = DbSet
            .Where(p => p.BusinessId == businessId)
            .Include(p => p.Taxes)
            .Include(p => p.Modifiers)
            .Include(p => p.Discounts.Where(d => d.ValidUntil >= DateTimeOffset.UtcNow))
            .OrderBy(p => p.CreatedAt)
            .AsQueryable();

        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ProductNotFoundErrorMessage(id);
    }
}
