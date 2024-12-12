using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.PaymentProcessing.Enums;

namespace PointOfSale.DataAccess.PaymentProcessing.Configurations
{
    public class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
    {
        private const string TableName = "OrderPayments";

        public void Configure(EntityTypeBuilder<OrderPayment> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .Property(o => o.Amount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder
                .Property(o => o.TotalPaid)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder
                .Property(o => o.PaymentStatus)
                .HasConversion<string>()
                .IsRequired();

            builder
                .Property(o => o.PaymentMethod)
                .HasConversion<string>()
                .IsRequired();

            builder
                .Property(o => o.OrderId)
                .IsRequired();

            builder
                .HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName);
        }
    }
}
