using PointOfSale.Api.DTOs;
using PointOfSale.DataAccess.Shared.Exceptions;

namespace PointOfSale.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var errorResponse = CreateErrorResponse(ex);

            context.Response.StatusCode = GetStatusCode(ex);
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    private object CreateErrorResponse(Exception ex)
    {
        var errorMessage = GetErrorMessage(ex);

        if (_environment.IsDevelopment())
        {
            return new DevelopmentErrorResponseDTO { ErrorMessage = errorMessage, StackTrace = ex.StackTrace };
        }

        return new ProductionErrorResponseDTO { ErrorMessage = errorMessage };
    }

    private static int GetStatusCode(Exception ex) =>
        ex switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

    private static string GetErrorMessage(Exception ex) =>
        ex switch
        {
            PointOfSaleException posEx => posEx.ErrorMessage.En,
            _ => "An unexpected error has occurred.",
        };
}
