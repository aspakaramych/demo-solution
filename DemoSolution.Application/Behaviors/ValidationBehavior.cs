using DemoSolution.Application.Abstractions;
using MediatR;

namespace DemoSolution.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IValidatable validatable)
        {
            var errors = validatable.Validate();
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }
        return await next();
    }
}