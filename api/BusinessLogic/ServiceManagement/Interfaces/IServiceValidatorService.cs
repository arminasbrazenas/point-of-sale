namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceValidatorService
{
    public Task<string> ValidateName(string name);
    public decimal ValidatePrice(decimal price);

}