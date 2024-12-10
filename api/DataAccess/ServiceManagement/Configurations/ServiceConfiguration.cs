using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    private const string TableName = "Services";

    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Name)
            .HasMaxLength(Constants.ServiceNameLength)
            .IsRequired();
        
        builder.Property(s => s.AvailableFrom)
            .IsRequired();
        
        builder.Property(s => s.AvailableTo)
            .IsRequired();
        
        builder.Property(s => s.Duration)
            .IsRequired();
        
        builder.Property(s => s.Price)
            .IsRequired();
        
        builder.ToTable(TableName, Constants.SchemaName);
    }
}