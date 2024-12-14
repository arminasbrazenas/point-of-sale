using System.Text.RegularExpressions;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;
using ValidationException = PointOfSale.BusinessLogic.Shared.Exceptions.ValidationException;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ContactInfoValidationService : IContactInfoValidationService
{
    public string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(new ContactInfoFirstOrLastNameEmptyErrorMessage());
        }

        if (name.Length > Constants.NameLastnameMaxLength)
        {
            throw new ValidationException(new ContactInfoFirstOrLastNameLengthErrorMessage(Constants.NameLastnameMaxLength));
        }
        
        return name;
    }


    public string ValidatePhoneNumber(string phoneNumber)
    {
        string pattern = @"^(?:\+370|8)[-\s]?\d{3}[-\s]?\d{5}$";

        if (!Regex.IsMatch(phoneNumber, pattern))
        {
            throw new ValidationException(new ContactInfoWrongNumberErrorMessage());
        }
        return phoneNumber;
    }
}