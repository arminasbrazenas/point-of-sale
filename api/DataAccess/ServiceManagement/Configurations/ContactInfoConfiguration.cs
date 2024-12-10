using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.ServiceManagement.Entities;

namespace PointOfSale.DataAccess.ServiceManagement.Configurations;

public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    private const string TableName = "ContactInfo";

    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.FirstName)
            .HasMaxLength(Constants.NameLastnameMaxLength)
            .IsRequired();
        
        builder.Property(c => c.LastName)
            .HasMaxLength(Constants.NameLastnameMaxLength)
            .IsRequired();
        
        builder.Property(c => c.PhoneNumber)
            .IsRequired();
        
        builder.ToTable(TableName, Constants.SchemaName);
    }
}