namespace Lab1.OptimizationContexts;

public record FibonacciOptimizationContext(
    double A,
    double B,
    double? X1 = null,
    double? X2 = null) : IOptimizationContext
{
    public bool ShouldExit { get; } = false;
}