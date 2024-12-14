using System.ComponentModel.DataAnnotations;
using PointOfSale.BusinessLogic.ServiceManagement.DTOs;
using PointOfSale.BusinessLogic.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.ServiceManagement.ErrorMessages;
using PointOfSale.DataAccess.ServiceManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ServiceManagement.Entities;
using PointOfSale.Models.ServiceManagement.Enums;
using ValidationException = PointOfSale.BusinessLogic.Shared.Exceptions.ValidationException;

namespace PointOfSale.BusinessLogic.ServiceManagement.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationMappingService _reservationMappingService;
    private readonly IReservationValidationService _reservationValidationService;
    private readonly IContactInfoService _contactInfoService;
    private readonly IServiceService _serviceService;
    private readonly IServiceAvailabilityRepository _serviceAvailabilityRepository;

    public ReservationService(
        IReservationMappingService reservationMappingService,
        IReservationValidationService reservationValidationService,
        IUnitOfWork unitOfWork,
        IReservationRepository reservationRepository,
        IContactInfoService contactInfoService,
        IServiceService serviceService,
        IServiceAvailabilityRepository serviceAvailabilityRepository)
    {
        _reservationMappingService = reservationMappingService;
        _reservationValidationService = reservationValidationService;
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
        _contactInfoService = contactInfoService;
        _serviceService = serviceService;
        _serviceAvailabilityRepository = serviceAvailabilityRepository;
    }

    public async Task<ReservationDTO> CreateReservation(CreateReservationDTO createReservationDto, CreateContactInfoDTO createContactInfoDto)
    {
        var contactInfo = await _contactInfoService.CreateContactInfo(createContactInfoDto);
        var service = await _serviceService.GetService(createReservationDto.ServiceId);

        var suitableServiceRecourseId =
            _serviceAvailabilityRepository.GetServiceAvailabilityByServiceId(createReservationDto.ServiceId)
                .Distinct()
                .Except(_reservationRepository.GetServiceResourceIdsByTime(createReservationDto.DateStart, createReservationDto.DateStart + service.Duration))
                .ToList();
        
        if (suitableServiceRecourseId is null)
        {
            throw new ValidationException(new ReservationResourceErrorMessage());
        }
        
        var dateStart = _reservationValidationService.ValidateCreateDateStart(createReservationDto.DateStart);
        var dateEnd = dateStart + service.Duration;
        var status = ReservationStatus.Confirmed;
        var serviceResourceId = _serviceAvailabilityRepository.GetServiceAvailabilityResourceIdByPriority(service.Id, suitableServiceRecourseId);
        var contactInfoId = contactInfo.Id;
        var serviceId = createReservationDto.ServiceId;
        var employeeId = createReservationDto.EmployeeId;
        var createdAt = DateTime.UtcNow;
        var lastUpdatedAt = createdAt;

        var reservation = new Reservation
        {
            DateStart = dateStart,
            DateEnd = dateEnd,
            Status = status,
            ServiceResourceId = serviceResourceId,
            ContactInfoId = contactInfoId,
            EmployeeId = employeeId,
            ServiceId = serviceId,
            CreatedAt = createdAt,
            LastUpdated = lastUpdatedAt
        };

        _reservationRepository.Add(reservation);
        await _unitOfWork.SaveChanges();
        
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> UpdateReservation(int reservationId, UpdateReservationDTO updateReservationDto)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        TimeSpan duration;
        bool timeOrServiceChanged = false;

        if (updateReservationDto.ServiceId is not null)
        {
            reservation.ServiceId = updateReservationDto.ServiceId.Value;
            var service = await _serviceService.GetService(updateReservationDto.ServiceId.Value);
            duration = service.Duration;
            timeOrServiceChanged = true;
        }
        else
        {
            var service = await _serviceService.GetService(reservation.ServiceId);
            duration = service.Duration;
        }
        
        if (updateReservationDto.DateStart is not null)
        {
            reservation.DateStart = updateReservationDto.DateStart.Value;
            reservation.DateEnd = reservation.DateStart + duration;
            timeOrServiceChanged = true;
        }

        if (timeOrServiceChanged)
        {
            var service = await _serviceService.GetService(reservation.ServiceId);

            var suitableServiceRecourseId =
                _serviceAvailabilityRepository.GetServiceAvailabilityByServiceId(reservation.ServiceId)
                    .Distinct()
                    .Except(_reservationRepository.GetServiceResourceIdsByTime(reservation.DateStart, reservation.DateEnd))
                    .ToList();

            if (suitableServiceRecourseId is null)
            {
                throw new ValidationException(new ReservationResourceErrorMessage());
            }
            
            reservation.ServiceResourceId = _serviceAvailabilityRepository.GetServiceAvailabilityResourceIdByPriority(service.Id, suitableServiceRecourseId);
        }

        if (updateReservationDto.ContactInfoId is not null)
        {
            reservation.ContactInfoId = updateReservationDto.ContactInfoId.Value;
        }

        if (updateReservationDto.EmployeeId is not null)
        {
            reservation.EmployeeId = updateReservationDto.EmployeeId.Value;
        }


        
        reservation.LastUpdated = DateTime.UtcNow;
        
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
        reservation.LastUpdated = DateTime.UtcNow;
        _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return _reservationMappingService.MapToReservationDTO(reservation);
    }

    public async Task<ReservationDTO> GetReservation(int reservationId)
    {
        var reservation = await _reservationRepository.Get(reservationId);
        return _reservationMappingService.MapToReservationDTO(reservation);
    }
}