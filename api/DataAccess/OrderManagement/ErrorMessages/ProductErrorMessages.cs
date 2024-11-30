using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class ProductNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _productId;

    public ProductNotFoundErrorMessage(int productId)
    {
        _productId = productId;
    }

    public string En => $"Product with id '{_productId}' not found.";
}

public class ProductNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Product name cannot be empty.";
}

public class ProductNameTooLongErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public ProductNameTooLongErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }

    public string En => $"Product name is too long. Max length is {_maxLength} characters.";
}

public class ProductNameConflictErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _name;

    public ProductNameConflictErrorMessage(string name)
    {
        _name = name;
    }

    public string En => $"Product with name '{_name}' already exists.";
}

public class ProductPriceNegativeErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Product price cannot be negative.";
}

public class ProductStockNegativeErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Product stock cannot be negative.";
}

public class ProductDuplicateTaxErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Same tax cannot be applied to the same product multiple times.";
}

public class ProductOutOfStockErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _productName;

    public ProductOutOfStockErrorMessage(string productName)
    {
        _productName = productName;
    }

    public string En => $"Product '{_productName}' is out of stock.";
}
