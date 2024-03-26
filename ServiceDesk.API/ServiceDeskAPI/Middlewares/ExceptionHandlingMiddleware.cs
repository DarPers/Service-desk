using System.Net;
using ServiceDeskAPI.ViewModels;
using ServiceDesk.Domain.Exceptions;

namespace ServiceDeskAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                EntityIsNullException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            }
        };

        _logger.LogError(error.ToString());

        return context.Response.WriteAsJsonAsync(error);
    }
}
