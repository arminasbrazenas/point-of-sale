using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs
{
    public class RefundRequestDTO
    {
        public required string ChargeId { get; set; }
        public required string PaymentIntentId { get; set; }
        public int? Amount { get; set; }
        public required string Reason { get; set; }
    }
}
