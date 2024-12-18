using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class ModifierNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _modifierId;

    public ModifierNotFoundErrorMessage(int modifierId)
    {
        _modifierId = modifierId;
    }

    public string En => $"Modifier with id '{_modifierId}' not found.";
}

public class IncompatibleProductModifierErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _productId;
    private readonly int _modifierId;

    public IncompatibleProductModifierErrorMessage(int productId, int modifierId)
    {
        _productId = productId;
        _modifierId = modifierId;
    }

    public string En => $"Modifier with id '{_modifierId}' is incompatible with product with id '{_productId}'.";
}

public class ModifierOutOfStockErrorMessage : IPointOfSaleErrorMessage
{
    private readonly string _modifierName;

    public ModifierOutOfStockErrorMessage(string modifierName)
    {
        _modifierName = modifierName;
    }

    public string En => $"Modifier '{_modifierName}' is out of stock.";
}

public class ModifierQuantityMustNotBeNegativeErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Modifier quantity must not be negative.";
}

public class ModifierNameMustNotBeEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Modifier name must not be empty.";
}

public class ModifierNameTooLongErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Modifier name is too long.";
}

public class ModifierNameConflictErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Modifier with such name already exists.";
}
