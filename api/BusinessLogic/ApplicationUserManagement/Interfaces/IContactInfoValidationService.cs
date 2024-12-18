namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;

public interface IContactInfoValidationService{
    void ValidateEmail(string email);
    void ValidatePhoneNumber(string phoneNumber);
}