using Lab1.Tools;

namespace Lab1.OptimizationContexts;

public record BrentOptimizationContext : IOptimizationContext
{
    public BrentOptimizationContext(double a, double b)
    {
        A = a;
        B = b;
        X = W = V = a + Constants.GoldenRatioProportion * (b - a);
        CurrentDistance = PreviousDistance = b - a;
    }

    public BrentOptimizationContext(
        double a, 
        double b, 
        double x, 
        double w, 
        double v,
        double currentDistance, 
        double previousDistance)
    {
        A = a;
        B = b;
        X = x;
        W = w;
        V = v;
        CurrentDistance = currentDistance;
        PreviousDistance = previousDistance;
    }

    public double A { get; }
    public double B { get; }
    public double X { get; }
    public double W { get; }
    public double V { get; }
    public double CurrentDistance { get; }
    public double PreviousDistance { get; }
    public bool ShouldExit { get; } = false;
}