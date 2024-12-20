using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Filters;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Repositories;

public class OrderRepository : RepositoryBase<Order, int>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<Order>> GetWithFilter(PaginationFilter paginationFilter, int businessId)
    {
        var query = DbSet.Where(o => o.BusinessId == businessId).AsQueryable().OrderByDescending(o => o.CreatedAt);
        return await GetPaged(query, paginationFilter);
    }

    public async Task<Order> GetWithOrderItems(int orderId)
    {
        var order = await DbSet
            .Where(o => o.Id == orderId)
            .Include(o => o.CreatedBy)
            .Include(o => o.Reservation)
            .ThenInclude(o => o.Employee)
            .Include(o => o.ServiceCharges)
            .ThenInclude(c => c.ModifiedBy)
            .Include(o => o.Discounts)
            .ThenInclude(d => d.ModifiedBy)
            .Include(o => o.Items)
            .ThenInclude(i => i.Modifiers)
            .Include(o => o.Items)
            .ThenInclude(i => i.Taxes)
            .Include(o => o.Items)
            .ThenInclude(i => i.Discounts)
            .ThenInclude(d => d.ModifiedBy)
            .FirstOrDefaultAsync();

        return order ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(orderId));
    }

    protected override IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(int id)
    {
        return new OrderNotFoundErrorMessage(id);
    }

    public async Task<int> GetTotalCount(int businessId)
    {
        return await DbSet.Where(o => o.BusinessId == businessId).CountAsync();
    }
}
