using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Filters;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationMappingService _reservationMappingService;
    private readonly IReservationValidationService _reservationValidationService;
    private readonly IServiceService _serviceService;
    private readonly IOrderManagementAuthorizationService _orderManagementAuthorizationService;

    public ReservationService(
        IReservationMappingService reservationMappingService,
        IReservationValidationService reservationValidationService,
        IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IServiceService serviceService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService
    )
    {
        _reservationMappingService = reservationMappingService;
        _reservationValidationService = reservationValidationService;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _serviceService = serviceService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
    }

    public async Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDto)
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(createReservationDto.BusinessId);

        var service = await _serviceService.GetService(createReservationDto.ServiceId);
        var dateStart = _reservationValidationService.ValidateDateStart(createReservationDto.StartDate);
        var dateEnd = dateStart + TimeSpan.FromMinutes(service.DurationInMinutes);
        var customerFirstname = _reservationValidationService.ValidateFirstName(
            createReservationDto.Customer.FirstName
        );
        var customerLastname = _reservationValidationService.ValidateLastName(createReservationDto.Customer.LastName);

        var reservation = new Reservation
        {
            Date = new ReservationDate { Start = dateStart, End = dateEnd },
            Status = ReservationStatus.Active,
            EmployeeId = createReservationDto.EmployeeId,
            ServiceId = createReservationDto.ServiceId,
            Customer = new ReservationCustomer { FirstName = customerFirstname, LastName = customerLastname },
            BusinessId = createReservationDto.BusinessId,
            Name = service.Name,
            Price = service.Price,
        };

        _reservationRepository.Add(reservation);
        await _unitOfWork.SaveChanges();

        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto)
    {
        var reservation = await _reservationRepository.GetWithRelatedData(reservationId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);

        if (reservation.Status != ReservationStatus.Active)
        {
            throw new ValidationException(new CannotModifyNonActiveReservationErrorMessage());
        }

        if (updateReservationDto.ServiceId is not null)
        {
            reservation.ServiceId = updateReservationDto.ServiceId.Value;
            var service = await _serviceService.GetService(updateReservationDto.ServiceId.Value);
            reservation.Name = service.Name;
            reservation.Price = service.Price;
        }

        if (updateReservationDto.EmployeeId is not null)
        {
            reservation.EmployeeId = updateReservationDto.EmployeeId.Value;
        }

        if (updateReservationDto.StartDate is not null)
        {
            reservation.Date.Start = _reservationValidationService.ValidateDateStart(
                updateReservationDto.StartDate.Value
            );
            reservation.Date.End = reservation.Date.Start + reservation.Service!.Duration;
        }

        if (updateReservationDto.Customer.FirstName is not null)
        {
            reservation.Customer.FirstName = _reservationValidationService.ValidateFirstName(
                updateReservationDto.Customer.FirstName
            );
        }

        if (updateReservationDto.Customer.LastName is not null)
        {
            reservation.Customer.LastName = _reservationValidationService.ValidateLastName(
                updateReservationDto.Customer.LastName
            );
        }

        await _unitOfWork.SaveChanges();

        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _reservationRepository.Delete(reservationId);
    }

    public async Task<ReservationDTO> CancelReservation(int reservationId)
    {
        var reservation = await _reservationRepository.GetWithRelatedData(reservationId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);

        if (reservation.Status != ReservationStatus.Active)
        {
            throw new ValidationException(new CannotCancelNonActiveReservationErrorMessage());
        }

        reservation.Status = ReservationStatus.Canceled;
        await _unitOfWork.SaveChanges();

        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> GetReservation(int reservationId)
    {
        var reservation = await _reservationRepository.GetWithRelatedData(reservationId);
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<PagedResponseDTO<ReservationDTO>> GetReservations(
        PaginationFilterDTO paginationFilterDTO,
        int businessId,
        ReservationFilter? filter
    )
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var reservations = await _reservationRepository.GetPaged(paginationFilter, businessId, filter);
        var totalCount = await _reservationRepository.GetTotalCount(businessId);
        return _reservationMappingService.MapToPagedReservationDTO(reservations, paginationFilter, totalCount);
    }
}
