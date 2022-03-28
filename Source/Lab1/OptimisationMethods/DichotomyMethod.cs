using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class DichotomyMethod : IOptimisationMethod<BoundedOptimizationContext>
{
    private readonly double _delta;

    public DichotomyMethod(double delta)
    {
        _delta = delta;
    }

    public BoundedOptimizationContext FindNewInterval(BoundedOptimizationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);
        var x1 = (a + b) / 2 - _delta;
        var x2 = (a + b) / 2 + _delta;

        if (function.Invoke(x1) < function.Invoke(x2))
        {
            return new BoundedOptimizationContext(a, x2);
        }
        if (function.Invoke(x2) < function.Invoke(x1))
        {
            return new BoundedOptimizationContext(x1, b);
        }

        return new BoundedOptimizationContext(x1, x2);
    }
}