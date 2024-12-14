using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.DataAccess.Shared;
using PointOfSale.Models.ServiceManagement.Entities;
using PointOfSale.Models.ServiceManagement.Enums;

namespace PointOfSale.DataAccess.ServiceManagement.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    private const string TableName = "Reservations";

    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.DateStart)
            .IsRequired();
        
        builder.Property(r => r.DateEnd)
            .IsRequired();
        
        builder.Property(r => r.Status)
            .HasConversion(new EnumToStringConverter<ReservationStatus>())
            .HasMaxLength(SharedConstants.EnumMaxLength)
            .IsRequired();
        
        builder.HasOne(r => r.ServiceResource)
            .WithMany(sr => sr.Reservations)
            .HasForeignKey(r => r.ServiceResourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.HasOne(r => r.ContactInfo)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ContactInfoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.LastUpdated);
        
        builder.ToTable(TableName, Constants.SchemaName);
    }
}