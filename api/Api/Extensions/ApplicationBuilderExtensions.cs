using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess;
using PointOfSale.DataAccess.Shared.Repositories;

namespace PointOfSale.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}
