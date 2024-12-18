using PointOfSale.BusinessLogic.BusinessManagement.DTOs;

namespace PointOfSale.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessValidationService
{
    Task ValidateCreateBusinessDTO(CreateBusinessDTO dto);
    void ValidateTime(int startHour, int startMinute, int endHour, int endMinute);
}
