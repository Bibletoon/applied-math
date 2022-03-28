using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class GoldenRatioMethod : IOptimisationMethod<BoundedOptimizationContext>
{
    private static readonly double GoldenRatioProportion = (1 + Math.Sqrt(5)) / 2;

    public BoundedOptimizationContext FindNewInterval(BoundedOptimizationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);
        var x1 = b - (b - a) / GoldenRatioProportion;
        var x2 = a + (b - a) / GoldenRatioProportion;

        if (function.Invoke(x1) >= function.Invoke(x2))
        {
            return new BoundedOptimizationContext(x1, b);
        }
        else
        {
            return new BoundedOptimizationContext(a, x2);
        }
    }
}