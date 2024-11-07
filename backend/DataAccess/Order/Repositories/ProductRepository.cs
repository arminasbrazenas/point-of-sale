using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Order.ErrorMessages;
using PointOfSale.DataAccess.Order.Interfaces;
using PointOfSale.DataAccess.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.DataAccess.Order.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Product> GetWithTaxes(int productId)
    {
        var product = await DbSet
            .Include(p => p.Taxes)
            .Where(p => p.Id == productId)
            .FirstOrDefaultAsync();

        return product ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
    }

    public Task<Product?> GetByNameOptional(string name)
    {
        return DbSet.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<List<Product>> GetPaginatedWithTaxes(PaginationFilter paginationFilter)
    {
        var query = DbSet.Include(p => p.Taxes).AsQueryable();
        return await GetPaginated(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ProductNotFoundErrorMessage(id);
    }
}
