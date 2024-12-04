using Microsoft.AspNetCore.Identity;
using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;
using PointOfSale.Models.BusinessManagement.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedServices(builder.Configuration, builder.Environment).AddOrderManagement();
builder.Services.AddSharedServices(builder.Configuration, builder.Environment).AddBusinessManagement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var roles = new[] { "Employee", "BusinessOwner", "Admin" };

    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new ApplicationRole(role));
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
