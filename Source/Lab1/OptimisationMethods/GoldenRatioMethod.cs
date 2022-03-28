using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class GoldenRatioMethod : IOptimisationMethod<GoldenRatioOptimisationContext>
{
    private static readonly double GoldenRatioProportion = (1 + Math.Sqrt(5)) / 2;

    public GoldenRatioOptimisationContext FindNewInterval(GoldenRatioOptimisationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);
        var x1 = context.X1 ?? b - (b - a) / GoldenRatioProportion;
        var x2 = context.X2 ?? a + (b - a) / GoldenRatioProportion;

        if (function.Invoke(x1) >= function.Invoke(x2))
        {
            return new GoldenRatioOptimisationContext(x1, b, x2, null);
        }
        else
        {
            return new GoldenRatioOptimisationContext(a, x2, null, x1);
        }
    }
}