using Microsoft.Extensions.Options;
using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;
using PointOfSale.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedServices(builder.Configuration, builder.Environment).AddOrderManagement();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
