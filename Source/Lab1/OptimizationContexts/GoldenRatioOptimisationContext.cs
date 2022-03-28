namespace Lab1.OptimizationContexts;

public record GoldenRatioOptimisationContext : IOptimizationContext
{
    public GoldenRatioOptimisationContext(double a, double b)
    {
        A = a;
        B = b;
    }

    public GoldenRatioOptimisationContext(double a, double b, double? x1, double? x2)
    {
        A = a;
        B = b;
        X1 = x1;
        X2 = x2;
    }

    public double A { get; }
    public double B { get; }
    public double? X1 { get; } = null;
    public double? X2 { get; } = null;
}