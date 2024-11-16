using PointOfSale.Models.OrderManagement.Entities;

namespace BusinessLogic.UnitTests.OrderManagement.TestUtilities;

public class ProductBuilder
{
    private readonly Product _product;

    public ProductBuilder()
    {
        _product = Activator.CreateInstance<Product>();
    }

    public Product Build() => _product;

    public ProductBuilder WithId(int id)
    {
        _product.Id = id;
        return this;
    }

    public ProductBuilder WithName(string name)
    {
        _product.Name = name;
        return this;
    }

    public ProductBuilder WithPrice(decimal price)
    {
        _product.Price = price;
        return this;
    }

    public ProductBuilder WithStock(int stock)
    {
        _product.Stock = stock;
        return this;
    }

    public ProductBuilder WithTaxes(List<Tax> taxes)
    {
        _product.Taxes = taxes;
        return this;
    }

    public ProductBuilder WithModifiers(List<Modifier> modifiers)
    {
        _product.Modifiers = modifiers;
        return this;
    }
}
