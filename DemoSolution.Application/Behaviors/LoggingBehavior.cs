using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoSolution.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        logger.LogInformation("→ {Request}", name);
        var sw = Stopwatch.StartNew();
        try
        {
            var response = await next();
            logger.LogInformation("← {Request} за {Ms} мс", name, sw.ElapsedMilliseconds);
            return response;
        }
        catch (Exception e)
        {
            logger.LogWarning("✗ {Request}: {Error}", name, e.Message);
            throw;
        }
    }
}