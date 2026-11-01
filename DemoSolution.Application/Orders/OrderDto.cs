namespace DemoSolution.Application.Orders;

public sealed record OrderDto(Guid Id, string Sku, int Quantity, DateTime CreatedAt, string Status);