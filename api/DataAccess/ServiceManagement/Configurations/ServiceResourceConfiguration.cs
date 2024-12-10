using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Configurations;

public class ServiceResourceConfiguration : IEntityTypeConfiguration<ServiceResource>
{
    private const string TableName = "ServiceResources";

    public void Configure(EntityTypeBuilder<ServiceResource> builder)
    {
        builder.HasKey(sr => sr.Id);
        
        builder.Property(sr => sr.Name)
            .HasMaxLength(Constants.ServiceResourceNameLength)
            .IsRequired();
        
        builder.ToTable(TableName, Constants.SchemaName);
    }
}