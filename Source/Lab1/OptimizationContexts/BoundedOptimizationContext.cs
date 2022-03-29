namespace Lab1.OptimizationContexts;

public record BoundedOptimizationContext(double A, double B) : IOptimizationContext
{
    public bool ShouldExit { get; } = false;
}