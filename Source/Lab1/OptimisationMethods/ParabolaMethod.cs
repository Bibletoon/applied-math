using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class ParabolaMethod : IOptimisationMethod<BoundedOptimizationContext>
{
    private Random _random = new Random();
    
    public BoundedOptimizationContext FindNewInterval(BoundedOptimizationContext context, Func<decimal, decimal> function)
    {
        var (a, b) = (context.A, context.B);
        decimal c = (decimal)_random.NextDouble() * (b - a) + a;

        decimal u = c - (((c - a) * (c - a)) * (function.Invoke(c) - function.Invoke(b)) -
                         ((c - b) * (c - b)) * (function.Invoke(c) - function.Invoke(a))) /
            (2 * ((c - a) * (function.Invoke(c) - function.Invoke(b)) -
                  (c - b) * (function.Invoke(c) - function.Invoke(a))));

        if (c < u)
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return new BoundedOptimizationContext(a, u);
            }
            else
            {
                return new BoundedOptimizationContext(c, b);
            }
        }
        else
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return new BoundedOptimizationContext(u, b);
            }
            else
            {
                return new BoundedOptimizationContext(a, c);
            }
        }
    }
}