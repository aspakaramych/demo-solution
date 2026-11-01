using DemoSolution.Application.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoSolution.Api;

[ApiController]
[Route("[controller]")]
public sealed class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await sender.Send(new GetOrderQuery(id), ct);
        return order is null ? NotFound() : Ok(order);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await sender.Send(new GetOrdersQuery(), ct));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        await sender.Send(new CancelOrderCommand(id), ct);
        return NoContent();
    }
}