using PointOfSale.Models.OrderManagement.Entities;

namespace BusinessLogic.UnitTests.OrderManagement.TestUtilities;

public class TaxBuilder
{
    private readonly Tax _tax;

    public TaxBuilder()
    {
        _tax = Activator.CreateInstance<Tax>();
    }

    public Tax Build() => _tax;

    public TaxBuilder WithId(int id)
    {
        _tax.Id = id;
        return this;
    }

    public TaxBuilder WithName(string name)
    {
        _tax.Name = name;
        return this;
    }

    public TaxBuilder WithRate(decimal rate)
    {
        _tax.Rate = rate;
        return this;
    }
}
