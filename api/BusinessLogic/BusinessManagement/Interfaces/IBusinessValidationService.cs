using PointOfSale.BusinessLogic.BusinessManagement.DTOs;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessValidationService
{
    Task ValidateCreateBusinessDTO(CreateBusinessDTO dto);
}
