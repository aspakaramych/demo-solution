namespace DemoSolution.Application.Abstractions;

public sealed class ValidationException(IEnumerable<string> errors) : Exception(string.Join("; ", errors))
{
    public IReadOnlyList<string> Errors { get; } = errors.ToList();
}