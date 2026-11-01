using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoSolution.Application.Orders;

public sealed record OrderCreatedEvent(Guid OrderId, string Sku, int Quantity) : INotification;
public sealed record OrderCancelledEvent(Guid OrderId) : INotification;

public sealed class InventoryHandler(ILogger<InventoryHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Inventory] резерв {Qty} x {Sku} для заказа {Id}",
            notification.Quantity, notification.Sku, notification.OrderId);
        return Task.CompletedTask;
    }
}

public sealed class AuditHandler(ILogger<AuditHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Audit] зафиксировано создание заказа {Id}", notification.OrderId);
        return Task.CompletedTask;
    }
}

public sealed class CancellationNotifier(ILogger<CancellationNotifier> logger) : INotificationHandler<OrderCancelledEvent>
{
    public Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Notify] заказ {Id} отменён", notification.OrderId);
        return Task.CompletedTask;
    }
}