using PointOfSale.Models.OrderManagement.Entities;

namespace BusinessLogic.UnitTests.OrderManagement.TestUtilities;

public class ModifierBuilder
{
    private readonly Modifier _modifier;

    public ModifierBuilder()
    {
        _modifier = Activator.CreateInstance<Modifier>();
    }

    public Modifier Build() => _modifier;

    public ModifierBuilder WithId(int id)
    {
        _modifier.Id = id;
        return this;
    }

    public ModifierBuilder WithName(string name)
    {
        _modifier.Name = name;
        return this;
    }

    public ModifierBuilder WithPrice(decimal price)
    {
        _modifier.Price = price;
        return this;
    }

    public ModifierBuilder WithStock(int stock)
    {
        _modifier.Stock = stock;
        return this;
    }
}
