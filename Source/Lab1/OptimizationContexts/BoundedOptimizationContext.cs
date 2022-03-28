namespace Lab1.OptimizationContexts;

public class BoundedOptimizationContext : IOptimizationContext
{
    public BoundedOptimizationContext(decimal a, decimal b)
    {
        A = a;
        B = b;
    }

    public decimal A { get; }
    public decimal B { get; }
}