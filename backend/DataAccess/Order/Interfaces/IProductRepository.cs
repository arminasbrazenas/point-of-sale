using PointOfSale.DataAccess.Order.Entities;
using PointOfSale.DataAccess.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Order.Interfaces;

public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<Product> GetWithTaxes(int productId);
    Task<Product?> GetByNameOptional(string name);
    Task<List<Product>> GetPaginatedWithTaxes(PaginationFilter paginationFilter);
}
