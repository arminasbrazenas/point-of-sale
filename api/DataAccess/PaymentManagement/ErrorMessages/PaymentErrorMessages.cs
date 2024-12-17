using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentManagement.ErrorMessages;

public class PaymentNotFoundErrorMessage(int paymentId) : IPointOfSaleErrorMessage
{
    public string En => $"Payment with id {paymentId} was not found.";
}

public class RefundNotFoundErrorMessage(int refundId) : IPointOfSaleErrorMessage
{
    public string En => $"PaymentRefund with id {refundId} was not found.";
}

public class OnlinePaymentNotFoundErrorMessage(string paymentId) : IPointOfSaleErrorMessage
{
    public string En => $"Online payment with id {paymentId} was not found.";
}

public class CannotPayForNonCompletedOrderErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot pay for order which is not completed.";
}

public class OrderIsNotFullyPaidErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Order is not fully paid.";
}

public class PaymentAmountMustBePositiveErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Payment amount must be positive.";
}

public class PaymentAmountExceedsOrderPriceErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Payment amount exceeds order's total price.";
}
