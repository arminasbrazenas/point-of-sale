using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.OrderManagement.ErrorMessages;

public class ReservationDateStartErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "This reservation time has already passed.";
}

public class ReservationWithoutServiceUpdateErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "This reservation can not be modified as its service no longer exists.";
}

public class CannotModifyNonActiveReservationErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Non-active reservation cannot be modified.";
}

public class ReservationNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _reservationId;

    public ReservationNotFoundErrorMessage(int reservationId)
    {
        _reservationId = reservationId;
    }

    public string En => $"Reservation with id '{_reservationId}' not found.";
}