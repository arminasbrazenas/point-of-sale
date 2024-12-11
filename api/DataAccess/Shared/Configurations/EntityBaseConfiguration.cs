using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Models.Shared.Entities;

public class EntityBaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById);

        builder.HasOne(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById);
    }
}
