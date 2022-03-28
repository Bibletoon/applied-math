namespace Lab1.OptimizationContexts;

public class ParabolaOptimisationContext : IOptimizationContext
{
    public ParabolaOptimisationContext(double a, double b)
    {
        A = a;
        B = b;
    }

    public ParabolaOptimisationContext(double a, double b, double? c)
    {
        A = a;
        B = b;
        C = c;
    }

    public double A { get; }
    public double B { get; }
    public double? C { get; }
}