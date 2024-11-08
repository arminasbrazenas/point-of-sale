using PointOfSale.Api.Extensions;
using PointOfSale.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedServices(builder.Configuration).AddOrderManagement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
