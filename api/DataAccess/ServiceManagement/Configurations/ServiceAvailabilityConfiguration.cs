using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Configurations;

public class ServiceAvailabilityConfiguration : IEntityTypeConfiguration<ServiceAvailability>
{
    private const string TableName = "ServiceAvailability";

    public void Configure(EntityTypeBuilder<ServiceAvailability> builder)
    {
        builder.HasKey(sa => sa.Id);
        
        builder.HasOne(sa => sa.Service)
            .WithMany(s => s.ServiceAvailability)
            .HasForeignKey(sa => sa.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(sa => sa.ServiceResource)
            .WithMany(sr => sr.ServiceAvailability)
            .HasForeignKey(sa => sa.ServiceResourceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(sa => sa.Priority)
            .HasMaxLength(Constants.ServiceNameLength)
            .IsRequired();
        
        builder.ToTable(TableName, Constants.SchemaName);
    }
}