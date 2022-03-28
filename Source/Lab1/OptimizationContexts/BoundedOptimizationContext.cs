namespace Lab1.OptimizationContexts;

public record BoundedOptimizationContext : IOptimizationContext
{
    public BoundedOptimizationContext(double a, double b)
    {
        A = a;
        B = b;
    }

    public double A { get; }
    public double B { get; }
}