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
