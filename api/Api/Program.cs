
using Microsoft.Extensions.Options;
using PointOfSale.DataAccess;
using Microsoft.AspNetCore.Identity;
using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;
using PointOfSale.Models.ApplicationUserManagement.Enums;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddSharedServices(builder.Configuration, builder.Environment)
    .AddApplicationUserManagement(builder.Configuration)
    .AddBusinessManagement()
    .AddOrderManagement()
    .AddPaymentManagement(builder.Configuration)
    .AddServiceManagement();;


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var roles = Enum.GetNames(typeof(Roles));

    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
