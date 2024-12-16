using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess.OrderManagement.Configurations;

public class ReservationConfiguration : EntityBaseConfiguration<Reservation, int>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(r => r.Date);

        builder.OwnsOne(r => r.Customer, nav =>
        {
            nav.Property(c => c.FirstName).HasMaxLength(Constants.CustomerFirstNameMaxLength).IsRequired();
            nav.Property(c => c.LastName).HasMaxLength(Constants.CustomerLastNameMaxLength).IsRequired();
        });

        builder
            .HasOne(r => r.Employee)
            .WithMany()
            .HasForeignKey(r => r.EmployeeId)
            .IsRequired();

        builder.ToTable("Reservations", Constants.SchemaName);
    }
}