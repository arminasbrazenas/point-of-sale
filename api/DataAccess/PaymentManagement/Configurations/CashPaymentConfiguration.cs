using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.PaymentManagement.Entities;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class CashPaymentConfiguration : EntityBaseConfiguration<CashPayment, int>
{
    public override void Configure(EntityTypeBuilder<CashPayment> builder)
    {
    }
}
