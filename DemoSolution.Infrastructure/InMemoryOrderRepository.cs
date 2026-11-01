using System.Collections.Concurrent;
using DemoSolution.Application.Abstractions;
using DemoSolution.Domain;

namespace DemoSolution.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public InMemoryOrderRepository()
    {
        var seed1 = new Order { Sku = "SKU-100", Quantity = 2 };
        var seed2 = new Order { Sku = "SKU-200", Quantity = 5 };
        _orders[seed1.Id] = seed1;
        _orders[seed2.Id] = seed2;
    }

    public Task AddAsync(Order order)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetAsync(Guid id) => Task.FromResult(_orders.TryGetValue(id, out var order) ? order : null);

    public Task<IReadOnlyList<Order>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<Order>>(_orders.Values.OrderBy(o => o.CreatedAt).ToList());

    public Task UpdateAsync(Order order)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}