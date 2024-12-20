using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Extensions;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Extensions;
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
    private readonly ISmsMessageService _iSmsMessageService;
    private readonly IBusinessRepository _businessRepository;

    public ReservationService(
        IReservationMappingService reservationMappingService,
        IReservationValidationService reservationValidationService,
        IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IServiceService serviceService,
        IOrderManagementAuthorizationService orderManagementAuthorizationService,
        ISmsMessageService iSmsMessageService,
        IBusinessRepository businessRepository
    )
    {
        _reservationMappingService = reservationMappingService;
        _reservationValidationService = reservationValidationService;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _serviceService = serviceService;
        _orderManagementAuthorizationService = orderManagementAuthorizationService;
        _iSmsMessageService = iSmsMessageService;
        _businessRepository = businessRepository;
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
        var customerPhoneNumber = _reservationValidationService.ValidatePhoneNumber(
            createReservationDto.Customer.PhoneNumber
        );
        var employeeId = _reservationValidationService.ValidateEmployeeId(service, createReservationDto.EmployeeId);
        var business = await _businessRepository.Get(createReservationDto.BusinessId);

        _reservationValidationService.ValidateWorkHours(
            business.WorkingHours.Start.TrimMilliseconds(),
            business.WorkingHours.End.TrimMilliseconds(),
            dateStart,
            dateEnd
        );

        var busyEmployeeIds = _reservationRepository.GetCreatingBusyEmployeeIdsByTime(
            createReservationDto.BusinessId,
            dateStart,
            dateEnd
        );

        if (busyEmployeeIds.Contains(employeeId))
        {
            throw new ValidationException(new EmployeeNotFreeErrorMessage());
        }

        var reservation = new Reservation
        {
            Date = new ReservationDate { Start = dateStart.TrimMilliseconds(), End = dateEnd.TrimMilliseconds() },
            Status = ReservationStatus.Active,
            EmployeeId = employeeId,
            ServiceId = createReservationDto.ServiceId,
            Customer = new ReservationCustomer
            {
                FirstName = customerFirstname,
                LastName = customerLastname,
                PhoneNumber = customerPhoneNumber,
            },
            BusinessId = createReservationDto.BusinessId,
            Name = service.Name,
            Price = service.Price,
            Notification = new ReservationNotification { IdempotencyKey = Guid.NewGuid(), SentAt = null },
        };

        _reservationRepository.Add(reservation);
        await _unitOfWork.SaveChanges();

        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto)
    {
        var reservation = await _reservationRepository.GetWithRelatedData(reservationId);

        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);

        bool dateServiceOrEmployeeChanged = false;

        if (reservation.Status != ReservationStatus.Active)
        {
            throw new ValidationException(new CannotModifyNonActiveReservationErrorMessage());
        }

        if (updateReservationDto.ServiceId is not null)
        {
            var service = await _serviceService.GetService(updateReservationDto.ServiceId.Value);
            _reservationValidationService.ValidateEmployeeId(
                service,
                updateReservationDto.EmployeeId ?? reservation.EmployeeId
            );

            reservation.ServiceId = updateReservationDto.ServiceId.Value;
            reservation.Name = service.Name;
            reservation.Price = service.Price;
            dateServiceOrEmployeeChanged = true;
        }

        if (updateReservationDto.EmployeeId is not null)
        {
            var service = await _serviceService.GetService(reservation.ServiceId!.Value);
            reservation.EmployeeId = _reservationValidationService.ValidateEmployeeId(
                service,
                updateReservationDto.EmployeeId.Value
            );
            dateServiceOrEmployeeChanged = true;
        }

        if (updateReservationDto.StartDate is not null)
        {
            reservation.Date.Start = _reservationValidationService.ValidateDateStart(
                updateReservationDto.StartDate.Value.TrimMilliseconds()
            );
            reservation.Date.End = (reservation.Date.Start + reservation.Service!.Duration).TrimMilliseconds();
            dateServiceOrEmployeeChanged = true;
        }

        if (updateReservationDto.Customer.FirstName is not null)
        {
            reservation.Customer = reservation.Customer with
            {
                FirstName = _reservationValidationService.ValidateFirstName(updateReservationDto.Customer.FirstName),
            };
        }

        if (updateReservationDto.Customer.LastName is not null)
        {
            reservation.Customer = reservation.Customer with
            {
                LastName = _reservationValidationService.ValidateLastName(updateReservationDto.Customer.LastName),
            };
        }

        if (
            updateReservationDto.Customer.PhoneNumber is not null
            && reservation.Customer.PhoneNumber != updateReservationDto.Customer.PhoneNumber
        )
        {
            reservation.Customer = reservation.Customer with
            {
                PhoneNumber = _reservationValidationService.ValidatePhoneNumber(
                    updateReservationDto.Customer.PhoneNumber
                ),
            };
            reservation.Notification = new ReservationNotification { IdempotencyKey = Guid.NewGuid(), SentAt = null };
        }

        if (dateServiceOrEmployeeChanged)
        {
            var business = await _businessRepository.Get(reservation.BusinessId);

            _reservationValidationService.ValidateWorkHours(
                business.WorkingHours.Start.TrimMilliseconds(),
                business.WorkingHours.End.TrimMilliseconds(),
                reservation.Date.Start,
                reservation.Date.End
            );

            var busyEmployeeIds = _reservationRepository.GetUpdatingBusyEmployeeIdsByTime(
                reservation.BusinessId,
                reservation.Id,
                reservation.Date.Start,
                reservation.Date.End
            );

            if (busyEmployeeIds.Contains(reservation.EmployeeId))
            {
                throw new ValidationException(new EmployeeNotFreeErrorMessage());
            }
        }

        await _unitOfWork.SaveChanges();

        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task DeleteReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);
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
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<PagedResponseDTO<ReservationDTO>> GetReservations(
        PaginationFilterDTO paginationFilterDTO,
        int businessId,
        ReservationFilter? filter
    )
    {
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(businessId);
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var reservations = await _reservationRepository.GetPaged(paginationFilter, businessId, filter);
        var totalCount = await _reservationRepository.GetTotalCount(businessId);
        return _reservationMappingService.MapToPagedReservationDTO(reservations, paginationFilter, totalCount);
    }

    public async Task CompleteReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);
        if (reservation.Status != ReservationStatus.InProgress)
        {
            throw new ValidationException(new ReservationIsNotInProgressErrorMessage());
        }

        reservation.Status = ReservationStatus.Completed;
        await _unitOfWork.SaveChanges();
    }

    public async Task MarkReservationInProgress(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);
        if (reservation.Status != ReservationStatus.Active)
        {
            throw new ValidationException(new ReservationAlreadyInProgressErrorMessage());
        }

        reservation.Status = ReservationStatus.InProgress;
        await _unitOfWork.SaveChanges();
    }

    public async Task RevertInProgressReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        await _orderManagementAuthorizationService.AuthorizeApplicationUser(reservation.BusinessId);
        if (reservation.Status != ReservationStatus.InProgress)
        {
            throw new ValidationException(new ReservationIsNotInProgressErrorMessage());
        }

        reservation.Status = ReservationStatus.Active;
        await _unitOfWork.SaveChanges();
    }

    public async Task SendUnsentNotifications()
    {
        var reservations = await _reservationRepository.GetWithUnsentNotifications();
        foreach (var reservation in reservations)
        {
            var message =
                $"You have booked an appointment at {reservation.Business.Name} for {reservation.Name}.\n"
                + $"Date: {FormatLocalDate(reservation.Date.Start)} - {FormatLocalDate(reservation.Date.End)}\n"
                + $"Employee: {reservation.Employee.FirstName} {reservation.Employee.LastName}";

            await _iSmsMessageService.SendSmsMessage(
                reservation.Customer.PhoneNumber,
                message,
                reservation.Notification.IdempotencyKey
            );

            reservation.Notification = reservation.Notification with { SentAt = DateTimeOffset.UtcNow };
            await _unitOfWork.SaveChanges();
        }
    }

    private static string FormatLocalDate(DateTimeOffset date) => date.LocalDateTime.ToString("MM/dd/yyyy HH:mm");
}
