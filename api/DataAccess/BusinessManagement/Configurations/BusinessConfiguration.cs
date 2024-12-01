using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.BusinessManagement.Entities;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    private const string TableName = "Businesses";

    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.HasKey(v => v.Id);

        builder.ToTable(TableName, Constants.SchemaName);
    }
}
