using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class OrderConfiguration : EntityBaseConfiguration<Order, int>
{
    private const string TableName = "Orders";

    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder
            .Property(o => o.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(o => o.ServiceCharges)
            .WithOne()
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(o => o.Business).WithMany().HasForeignKey(o => o.BusinessId).IsRequired();
        builder
            .HasMany(o => o.Discounts)
            .WithOne()
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(o => o.Reservation)
            .WithOne()
            .HasForeignKey<Order>(o => o.ReservationId)
            .IsRequired(false);

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
