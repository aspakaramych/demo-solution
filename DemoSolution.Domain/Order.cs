namespace DemoSolution.Domain;

public sealed class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Sku { get; init; } = "";
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public string Status { get; private set; } = "Created";

    public void Cancel()
    {
        if (Status == "Cancelled")
            throw new InvalidOperationException("Заказ уже отменён");
        Status = "Cancelled";
    }
}