namespace PointOfSale.Api.Middlewares;

public class HTTPLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HTTPLoggingMiddleware> _logger;

    public HTTPLoggingMiddleware(RequestDelegate next, ILogger<HTTPLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var requestLog = $"Request: Method={request.Method}, Path={request.Path}, Query={request.QueryString}";

        var originalResponseBodyStream = context.Response.Body;
        await using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        var response = context.Response;
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();
        responseBodyStream.Seek(0, SeekOrigin.Begin);

        var responseLog = $"Response: StatusCode={response.StatusCode}";

        _logger.LogInformation($"{requestLog} | {responseLog}");

        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
    }
}
