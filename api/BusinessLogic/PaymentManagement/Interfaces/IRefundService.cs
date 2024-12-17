using PointOfSale.BusinessLogic.PaymentManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces
{
    public interface IRefundService
    {
        Task<RefundResponseDTO> RefundPaymentAsync(RefundRequestDTO refundRequest);
        Task<RefundResponseDTO> GetRefundDetailsAsync(string refundId);
    }
}
