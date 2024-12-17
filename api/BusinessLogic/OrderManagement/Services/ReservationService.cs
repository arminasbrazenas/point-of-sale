using PointOfSale.BusinessLogic.OrderManagement.DTOs;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.Shared.DTOs;
using PointOfSale.BusinessLogic.Shared.Exceptions;
using PointOfSale.BusinessLogic.Shared.Factories;
using PointOfSale.DataAccess.OrderManagement.ErrorMessages;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.OrderManagement.ValueObjects;
using Stripe;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationMappingService _reservationMappingService;
    private readonly IReservationValidationService _reservationValidationService;
    private readonly IServiceService _serviceService;

    public ReservationService(
        IReservationMappingService reservationMappingService,
        IReservationValidationService reservationValidationService,
        IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IServiceService serviceService)
    {
        _reservationMappingService = reservationMappingService;
        _reservationValidationService = reservationValidationService;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _serviceService = serviceService;
    }

    public async Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDto)
    {
        var service = await _serviceService.GetService(createReservationDto.ServiceId);
        
        
        var dateStart = _reservationValidationService.ValidateDateStart(createReservationDto.StartDate);
        var dateEnd = dateStart + service.Duration;
        var status = ReservationStatus.Active;
        var serviceId = createReservationDto.ServiceId;
        var employeeId = createReservationDto.EmployeeId;
        var businessId = createReservationDto.BusinessId;
        var customerFirstname = _reservationValidationService.ValidateFirstName(createReservationDto.Customer.FirstName);
        var customerLastname = _reservationValidationService.ValidateLastName(createReservationDto.Customer.LastName);


        var reservation = new Reservation
        {
            Date = new ReservationDate
            {
                Start = dateStart,
                End = dateEnd
            },
            Status = status,
            EmployeeId = employeeId,
            ServiceId = serviceId,
            Customer = new ReservationCustomer
            {
                FirstName = customerFirstname,
                LastName = customerLastname
            },
            BusinessId = businessId,
            Name = service.Name,
            Price = service.Price,
        };

        _reservationRepository.Add(reservation);
        await _unitOfWork.SaveChanges();
        
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto)
    {
        var reservation = await _reservationRepository.Get(reservationId);

        if (reservation.Status != ReservationStatus.Active)
        {
            throw new ValidationException(new UpdateCancelledReservationErrorMessage());
        }

        if (reservation.Date.Start < DateTimeOffset.Now)
        {
            throw new ValidationException(new UpdatePassedReservationErrorMessage());
        }
        
        TimeSpan duration;

        if (updateReservationDto.ServiceId is not null)
        {
            reservation.ServiceId = updateReservationDto.ServiceId.Value;
            var service = await _serviceService.GetService(updateReservationDto.ServiceId.Value);
            duration = service.Duration;
            reservation.Name = service.Name;
            reservation.Price = service.Price;
        }
        else if (reservation.ServiceId is not null)
        {
            var service = await _serviceService.GetService(reservation.ServiceId.Value);
            duration = service.Duration;
        }
        else
        {
            throw new ValidationException(new ReservationWithoutServiceUpdateErrorMessage());
        }
        
        if (updateReservationDto.StartDate is not null)
        {
            reservation.Date.Start = updateReservationDto.StartDate.Value;
            reservation.Date.End = reservation.Date.Start + duration;
        }
        

        if (updateReservationDto.Customer is not null)
        {
            reservation.Customer = updateReservationDto.Customer;
        }

        if (updateReservationDto.EmployeeId is not null)
        {
            reservation.EmployeeId = updateReservationDto.EmployeeId.Value;
        }
        
        
        _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _reservationRepository.Delete(reservationId);
    }

    public async Task<ReservationDTO> CancelReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        reservation.Status = ReservationStatus.Cancelled;
        _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> GetReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        return _reservationMappingService.MapToReservationDTO(reservation);
    }
    
    public async Task<PagedResponseDTO<ReservationDTO>> GetReservations(PaginationFilterDTO paginationFilterDTO)
    {
        var paginationFilter = PaginationFilterFactory.Create(paginationFilterDTO);
        var reservations = await _reservationRepository.GetPaged(paginationFilter);
        var totalCount = await _reservationRepository.GetTotalCount();
        return _reservationMappingService.MapToPagedReservationDTO(reservations, paginationFilter, totalCount);
    }
}