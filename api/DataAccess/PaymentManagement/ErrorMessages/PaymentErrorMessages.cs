using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.PaymentManagement.ErrorMessages;

public class PaymentNotFoundErrorMessage(int paymentId) : IPointOfSaleErrorMessage
{
    public string En => $"Payment with id {paymentId} was not found.";
}

public class CardPaymentNotFoundErrorMessage(string paymentId) : IPointOfSaleErrorMessage
{
    public string En => $"Card payment with id {paymentId} was not found.";
}

public class CannotPayForNonCompletedOrderErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot pay for order which is not completed.";
}

public class CannotRefundNonClosedOrderErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot refund order which is not closed.";
}

public class CannotRefundGiftCardPaymentErrorMessage : IPointOfSaleErrorMessage
{
    public string En => "Cannot refund gift card payment.";
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
