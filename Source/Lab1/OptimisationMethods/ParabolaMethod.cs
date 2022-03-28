using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public class ParabolaMethod : IOptimisationMethod<BoundedOptimizationContext>
{
    private readonly Random _random = new Random();
    private double _c;
    private bool _firstIteration = true;
    
    public BoundedOptimizationContext FindNewInterval(BoundedOptimizationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);

        if (_firstIteration)
        {
            _c = _random.NextDouble() * (b - a) + a;
            _firstIteration = false;
        }

        double u = _c - (((_c - a) * (_c - a)) * (function.Invoke(_c) - function.Invoke(b)) -
                         ((_c - b) * (_c - b)) * (function.Invoke(_c) - function.Invoke(a))) /
            (2 * ((_c - a) * (function.Invoke(_c) - function.Invoke(b)) -
                  (_c - b) * (function.Invoke(_c) - function.Invoke(a))));

        if (_c < u)
        {
            if (function.Invoke(_c) < function.Invoke(u))
            {
                return new BoundedOptimizationContext(a, u);
            }
            else
            {
                var c = _c;
                _c = u;
                if (u <= c || u >= b)
                    _firstIteration = true;
                
                return new BoundedOptimizationContext(_c, b);
            }
        }
        else
        {
            if (function.Invoke(_c) < function.Invoke(u))
            {
                return new BoundedOptimizationContext(u, b);
            }
            else
            {
                var c = _c;
                _c = u;
                if (u <= c || u >= b)
                    _firstIteration = true;
                
                return new BoundedOptimizationContext(a, _c);
            }
        }
    }
}