using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ReservationConfiguration : EntityBaseConfiguration<Reservation, int>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(r => r.Date);

        builder.OwnsOne(
            r => r.Customer,
            nav =>
            {
                nav.Property(c => c.FirstName).HasMaxLength(Constants.CustomerFirstNameMaxLength).IsRequired();
                nav.Property(c => c.LastName).HasMaxLength(Constants.CustomerLastNameMaxLength).IsRequired();
            }
        );

        builder.OwnsOne(r => r.Notification);

        builder
            .Property(r => r.Status)
            .HasConversion(new EnumToStringConverter<ReservationStatus>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();

        builder.HasOne(r => r.Employee).WithMany().HasForeignKey(r => r.EmployeeId).IsRequired();

        builder.HasOne(r => r.Service).WithMany().HasForeignKey(r => r.ServiceId).OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Business).WithMany().HasForeignKey(r => r.BusinessId).IsRequired();

        builder.Property(r => r.Name).HasMaxLength(Constants.ServiceNameMaxLength).IsRequired();

        builder
            .Property(r => r.Price)
            .HasPrecision(SharedConstants.MoneyPrecision, SharedConstants.MoneyScale)
            .IsRequired();

        builder.ToTable("Reservations", Constants.SchemaName);
    }
}
