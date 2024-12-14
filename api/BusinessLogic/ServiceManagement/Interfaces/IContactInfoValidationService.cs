namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IContactInfoValidationService
{
    public string ValidateName(string name);
    public string ValidatePhoneNumber(string phoneNumber);
}