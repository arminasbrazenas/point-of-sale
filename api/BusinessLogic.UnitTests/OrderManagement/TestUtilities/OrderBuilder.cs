using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace BusinessLogic.UnitTests.OrderManagement.TestUtilities;

public class OrderBuilder
{
    private readonly Order _order;

    public OrderBuilder()
    {
        _order = Activator.CreateInstance<Order>();
    }

    public Order Build() => _order;

    public OrderBuilder WithId(int id)
    {
        _order.Id = id;
        return this;
    }

    public OrderBuilder WithStatus(OrderStatus status)
    {
        _order.Status = status;
        return this;
    }

    public OrderBuilder WithItems(List<OrderItem> orderItems)
    {
        _order.Items = orderItems;
        return this;
    }
}
