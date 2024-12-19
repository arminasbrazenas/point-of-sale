using Microsoft.AspNetCore.Identity;
using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;
using PointOfSale.BusinessLogic.ApplicationUserManagement.DTOs;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Enums;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder
    .Services.AddSharedServices(builder.Configuration, builder.Environment)
    .AddApplicationUserManagement(builder.Configuration)
    .AddBusinessManagement()
    .AddOrderManagement()
    .AddPaymentManagement(builder.Configuration);

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

    var userService = scope.ServiceProvider.GetRequiredService<IApplicationUserService>();
    var adminDto = new RegisterApplicationUserDTO(
        FirstName: builder.Configuration["AdminSettings:ADMIN_FIRST_NAME"]!,
        LastName: builder.Configuration["AdminSettings:ADMIN_LAST_NAME"]!,
        Email: builder.Configuration["AdminSettings:ADMIN_EMAIL"]!,
        PhoneNumber: builder.Configuration["AdminSettings:ADMIN_PHONE_NUMBER"]!,
        Password: builder.Configuration["AdminSettings:ADMIN_PASSWORD"]!,
        BusinessId: null,
        Role: "Admin"
    );

    await userService.CreateAdminUser(adminDto);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<HTTPLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var lifetimeLogger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("ApplicationLifecycle");

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStarted.Register(() =>
{
    lifetimeLogger.LogInformation("Application has started at {Time}", DateTime.UtcNow);
});

lifetime.ApplicationStopping.Register(() =>
{
    lifetimeLogger.LogInformation("Application is stopping at {Time}", DateTime.UtcNow);
});

lifetime.ApplicationStopped.Register(() =>
{
    lifetimeLogger.LogInformation("Application has stopped at {Time}", DateTime.UtcNow);
});

app.Run();
