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

    public async Task<Product> GetWithTaxes(int productId)
    {
        var product = await DbSet.Include(p => p.Taxes).Where(p => p.Id == productId).FirstOrDefaultAsync();

        return product ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
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

    public async Task<List<Product>> GetPaginatedWithTaxes(PaginationFilter paginationFilter)
    {
        var query = DbSet.Include(p => p.Taxes).AsQueryable();
        return await GetPaged(query, paginationFilter);
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new ProductNotFoundErrorMessage(id);
    }
}
