using System.Text.RegularExpressions;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.DataAccess.ApplicationUserManagement.ErrorMessages;

public class ContactInfoValidationService :IContactInfoValidationService
{
    public void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ValidationException(new InvalidApplicationUserCredentialsErrorMessage());

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(email))
        {
            throw new ValidationException(new InvalidApplicationUserCredentialsErrorMessage());
        }
    }
    public void ValidatePhoneNumber(string phoneNumber){
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ValidationException(new InvalidPhoneNumberErrorMessage());

        string pattern = @"^\+?[0-9]{10,15}$";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(phoneNumber))
        {
            throw new ValidationException(new InvalidPhoneNumberErrorMessage());
        }
    }
}