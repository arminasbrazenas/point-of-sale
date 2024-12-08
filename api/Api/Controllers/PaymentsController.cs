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
    public async Task<ActionResult<CashPaymentDTO>> PayByCash([FromBody] PayByCashDTO payByCashDTO)
    {
        var cashPayment = await _paymentService.PayByCash(payByCashDTO);
        return Ok(cashPayment);
    }

    [HttpPost]
    [Route("gift-card")]
    public async Task<ActionResult<GiftCardPaymentDTO>> PayByCash([FromBody] PayByGiftCardDTO payByGiftCardDTO)
    {
        var giftCardPayment = await _paymentService.PayByGiftCard(payByGiftCardDTO);
        return Ok(giftCardPayment);
    }

    [HttpPost]
    [Route("tips")]
    public async Task<ActionResult<TipDTO>> AddTip([FromBody] AddTipDTO addTipDTO)
    {
        var tip = await _paymentService.AddTip(addTipDTO);
        return Ok(tip);
    }

    [HttpGet]
    [Route("tips")]
    public async Task<ActionResult<List<TipDTO>>> GetTips([FromQuery] int orderId)
    {
        var tips = await _paymentService.GetTips(orderId);
        return Ok(tips);
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
