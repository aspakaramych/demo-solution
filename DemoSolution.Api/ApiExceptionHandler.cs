using DemoSolution.Application.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DemoSolution.Api;

public sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Ошибка валидации"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Не найдено"),
            InvalidOperationException => (StatusCodes.Status409Conflict, "Конфликт состояний"),
            _ => (StatusCodes.Status500InternalServerError, "Внутренняяя ошибка"),
        };
        
        if (status == 500)
            logger.LogError(exception, "Необработанная ошибка");
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = status == 500 ? null : exception.Message,
            Extensions = exception is ValidationException v
                ? new Dictionary<string, object?>() { ["errors"] = v.Errors }
                : [],
        }, cancellationToken);
        return true;
    }
}