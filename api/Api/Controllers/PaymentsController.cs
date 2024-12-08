using Microsoft.AspNetCore.Mvc;
using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.Controllers;

[ApiController]
[Route("v1/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [Route("cash")]
    public async Task<ActionResult<CashPaymentDTO>> PayByCash([FromBody] CreatePaymentDTO createPaymentDTO)
    {
        var cashPayment = await _paymentService.PayByCash(createPaymentDTO);
        return Ok(cashPayment);
    }

    [HttpPost]
    [Route("complete")]
    public async Task<ActionResult<CashPaymentDTO>> CompleteOrderPayments(
        [FromBody] CompleteOrderPaymentsDTO completeOrderPaymentsDTO
    )
    {
        await _paymentService.CompleteOrderPayments(completeOrderPaymentsDTO);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<OrderPaymentsDTO>> GetOrderPayments([FromQuery] int orderId)
    {
        var orderPayments = await _paymentService.GetOrderPayments(orderId);
        return Ok(orderPayments);
    }
}
