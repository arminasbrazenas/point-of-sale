using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.PaymentManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.DataAccess.PaymentManagement.Configurations;

public class PaymentConfiguration : EntityBaseConfiguration<Payment, int>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Amount)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder
            .Property(p => p.Method)
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .HasConversion(new EnumToStringConverter<PaymentMethod>())
            .IsRequired();

        builder
            .Property(p => p.Status)
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .HasConversion(new EnumToStringConverter<PaymentStatus>())
            .IsRequired();

        builder.HasOne(p => p.Order).WithMany().HasForeignKey(p => p.OrderId).IsRequired();

        builder
            .HasDiscriminator(p => p.Method)
            .HasValue<CashPayment>(PaymentMethod.Cash)
            .HasValue<GiftCardPayment>(PaymentMethod.GiftCard)
            .HasValue<CardPayment>(PaymentMethod.Card);

        builder
            .HasOne(o => o.Business)
            .WithMany()
            .HasForeignKey(o => o.BusinessId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        builder
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        builder.ToTable("OrderPayments", Constants.SchemaName);
    }
}
