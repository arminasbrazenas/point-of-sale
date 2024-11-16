using PointOfSale.Models.OrderManagement.Entities;

namespace BusinessLogic.UnitTests.OrderManagement.TestUtilities;

public class OrderItemBuilder
{
    private readonly OrderItem _orderItem;

    public OrderItemBuilder()
    {
        _orderItem = Activator.CreateInstance<OrderItem>();
    }

    public OrderItem Build() => _orderItem;

    public OrderItemBuilder WithProductId(int productId)
    {
        _orderItem.ProductId = productId;
        return this;
    }

    public OrderItemBuilder WithQuantity(int quantity)
    {
        _orderItem.Quantity = quantity;
        return this;
    }

    public OrderItemBuilder WithModifiers(List<OrderItemModifier> modifiers)
    {
        _orderItem.Modifiers = modifiers;
        return this;
    }
}
