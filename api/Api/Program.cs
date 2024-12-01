using Microsoft.AspNetCore.Identity;
using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedServices(builder.Configuration, builder.Environment).AddOrderManagement();
builder.Services.AddSharedServices(builder.Configuration, builder.Environment).AddBusinessManagement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var roles = new[] { "Admin", "BusinessOwner", "Employee" };
    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }
    }
}

app.MapControllers();

app.Run();
