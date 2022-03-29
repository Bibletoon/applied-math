namespace Lab1.OptimizationContexts;

public record ParabolaOptimisationContext : IOptimizationContext
{
    public ParabolaOptimisationContext(double a, double b)
    {
        A = a;
        B = b;
        ShouldExit = false;
    }

    public ParabolaOptimisationContext(double a, double b, double? c, bool shouldExit = false)
    {
        A = a;
        B = b;
        C = c;
        ShouldExit = shouldExit;
    }

    public double A { get; }
    public double B { get; }
    public double? C { get; }
    public bool ShouldExit { get; }
}