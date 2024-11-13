using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<Product> GetWithTaxes(int productId);
    Task<Product> GetWithModifiers(int productId);
    Task<Product?> GetByNameOptional(string name);
    Task<List<Product>> GetPaginatedWithTaxes(PaginationFilter paginationFilter);
}
