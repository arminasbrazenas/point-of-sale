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

app.MapControllers();

app.Run();
