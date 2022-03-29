using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class ParabolaMethod : IOptimisationMethod<ParabolaOptimisationContext>
{
    public double Accuracy { get; set; }
    private readonly Random _random = new Random();

    public string Title => "Parabola Method";

    public ParabolaOptimisationContext FindNewInterval(ParabolaOptimisationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);

        var c = context.C ?? _random.NextDouble() * (b - a) + a;

        double u = c - (((c - a) * (c - a)) * (function.Invoke(c) - function.Invoke(b)) -
                        ((c - b) * (c - b)) * (function.Invoke(c) - function.Invoke(a))) /
            (2 * ((c - a) * (function.Invoke(c) - function.Invoke(b)) -
                  (c - b) * (function.Invoke(c) - function.Invoke(a))));

        if (context.U is not null && Math.Abs(u - context.U.Value) < Accuracy)
        {
            return new ParabolaOptimisationContext(u, u, true);
        }
        
        if (c < u)
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return new ParabolaOptimisationContext(a, u, c, u);
            }
            else
            {
                double? cache = (u <= c || u >= b) ? null : u;

                return new ParabolaOptimisationContext(c, b, cache, u);
            }
        }
        else
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return new ParabolaOptimisationContext(u, b, c, u);
            }
            else
            {
                double? cache = (u <= a || u >= c) ? null : u;

                return new ParabolaOptimisationContext(a, c, cache, u);
            }
        }
    }
}