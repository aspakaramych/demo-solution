namespace DemoSolution.Application.Abstractions;

public interface IValidatable
{
    IReadOnlyList<string> Validate();
}