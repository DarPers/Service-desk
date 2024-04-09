using System.Net;
using WarehouseAPI.ViewModels;

namespace WarehouseAPI.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ErrorModel
        {
            Message = exception.Message,
            ExceptionType = exception.GetType().Name,
            StackTrace = exception.StackTrace,
            StatusCode = exception switch
            {
                NullReferenceException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            }
        };

        _logger.LogError(error.ToString());

        return context.Response.WriteAsJsonAsync(error);
    }
}
