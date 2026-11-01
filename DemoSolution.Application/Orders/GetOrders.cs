using DemoSolution.Application.Abstractions;
using MediatR;

namespace DemoSolution.Application.Orders;

public sealed record GetOrderQuery(Guid Id) : IRequest<OrderDto?>;

public sealed class GetOrderHandler(IOrderRepository repository)
    : IRequestHandler<GetOrderQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = await repository.GetAsync(query.Id);
        return order is null ? null : new OrderDto(order.Id, order.Sku, order.Quantity, order.CreatedAt, order.Status);
    }
}

public sealed record GetOrdersQuery : IRequest<IReadOnlyList<OrderDto>>;

public sealed class GetOrdersHandler(IOrderRepository repository)
    : IRequestHandler<GetOrdersQuery, IReadOnlyList<OrderDto>>
{
    public async Task<IReadOnlyList<OrderDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken) =>
        (await repository.GetAllAsync())
        .Select(o => new OrderDto(o.Id, o.Sku, o.Quantity, o.CreatedAt, o.Status))
        .ToList();
}