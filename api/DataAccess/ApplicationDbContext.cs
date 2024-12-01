using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.BusinessManagement.Configurations;
using PointOfSale.DataAccess.Shared.Interceptors;
using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.OrderManagement.Entities;

namespace PointOfSale.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private static readonly AuditingInterceptor AuditingInterceptor = new();

    public DbSet<Tax> Taxes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Modifier> Modifiers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderItemModifier> OrderItemModifiers { get; set; }
    public DbSet<OrderItemTax> OrderItemTaxes { get; set; }
    public DbSet<ServiceCharge> ServiceCharges { get; set; }
    public DbSet<Business> Businesses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(AuditingInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
