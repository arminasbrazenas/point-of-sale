using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.ServiceManagement.ErrorMessages;

public class ContactInfoFirstOrLastNameEmptyErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Contact info Firstname and Lastname cannot be empty.";
}

public class ContactInfoFirstOrLastNameLengthErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _maxLength;

    public ContactInfoFirstOrLastNameLengthErrorMessage(int maxLength)
    {
        _maxLength = maxLength;
    }
    public string En => $"Contact info Firstname and Lastname cannot be more than {_maxLength} characters long.";
}

public class ContactInfoWrongNumberErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Contact info phone number format pattern must be: +370 XXX XXXXX or 8 XXX XXXXX.";
}