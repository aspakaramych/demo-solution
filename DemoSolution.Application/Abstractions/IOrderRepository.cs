using DemoSolution.Domain;

namespace DemoSolution.Application.Abstractions;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetAsync(Guid id);
    Task<IReadOnlyList<Order>> GetAllAsync();
    Task UpdateAsync(Order order);
}