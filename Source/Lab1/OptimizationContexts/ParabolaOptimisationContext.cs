namespace Lab1.OptimizationContexts;

public record ParabolaOptimisationContext : IOptimizationContext
{
    public ParabolaOptimisationContext(double a, double b)
    {
        A = a;
        B = b;
        ShouldExit = false;
    }

    public ParabolaOptimisationContext(double a, double b, bool shouldExit = false)
    {
        A = a;
        B = b;
        ShouldExit = shouldExit;
    }

    public ParabolaOptimisationContext(double a, double b, double? c, double? u)
    {
        A = a;
        B = b;
        C = c;
        U = u;
    }

    public double A { get; }
    public double B { get; }
    public double? C { get; }
    public double? U { get; }
    public bool ShouldExit { get; }
}