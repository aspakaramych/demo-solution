using DemoSolution.Application.Abstractions;
using MediatR;

namespace DemoSolution.Application.Orders;

public sealed record CancelOrderCommand(Guid Id) : IRequest;

public sealed class CancelOrderHandler(IOrderRepository repository, IPublisher publisher)
    : IRequestHandler<CancelOrderCommand>
{
    public async Task Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await repository.GetAsync(command.Id)
                    ?? throw new KeyNotFoundException($"Заказ {command.Id} не найден");

        order.Cancel(); 
        await repository.UpdateAsync(order);
        await publisher.Publish(new OrderCancelledEvent(order.Id), cancellationToken);
    }
}