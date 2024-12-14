namespace PointOfSale.BusinessLogic.ServiceManagement.Interfaces;

public interface IServiceResourceValidatorService
{
    public Task<string> ValidateName(string name);
}