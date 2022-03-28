namespace Lab1.OptimizationContexts;

public class BoundedOptimizationContext : IOptimizationContext
{
    public BoundedOptimizationContext(double a, double b)
    {
        A = a;
        B = b;
    }

    public double A { get; }
    public double B { get; }
}