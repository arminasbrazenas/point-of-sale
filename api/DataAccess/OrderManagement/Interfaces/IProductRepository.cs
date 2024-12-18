using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Interfaces;

public interface IProductRepository : IRepositoryBase<Product, int>
{
    Task<Product> GetWithRelatedData(int productId);
    Task<List<Product>> GetManyWithRelatedData(IEnumerable<int> productIds);
    Task<Product?> GetByNameOptional(string name, int businessId);
    Task<List<Product>> GetPaged(PaginationFilter paginationFilter, int businessId);
    Task<int> GetTotalCount(int businessId);
}
