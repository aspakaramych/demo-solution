using DemoSolution.Application.Abstractions;
using DemoSolution.Domain;
using MediatR;

namespace DemoSolution.Application.Orders;

public sealed record CreateOrderCommand(string Sku, int Quantity) : IRequest<Guid>, IValidatable
{
    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(Sku))
            errors.Add("Sku обязателен");
        if (Quantity <= 0)
            errors.Add("Quantity должно быть больше нуля");
        return errors;
    }
}

public sealed class CreateOrderHandler(IOrderRepository repository, IPublisher publisher)
    : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order { Sku = command.Sku, Quantity = command.Quantity };
        await repository.AddAsync(order);

        await publisher.Publish(new OrderCreatedEvent(order.Id, order.Sku, order.Quantity), cancellationToken);
        return order.Id;
    }
}