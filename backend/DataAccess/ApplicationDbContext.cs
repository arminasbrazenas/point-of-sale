using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Shared.Interceptors;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess;

public class ApplicationDbContext : DbContext
{
    private static readonly AuditingInterceptor AuditingInterceptor = new();

    public DbSet<Tax> Taxes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemTax> OrderItemTaxes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(AuditingInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
