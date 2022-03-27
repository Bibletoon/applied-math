namespace Lab1.OptimisationMethods;

public class ParabolaMethod : IOptimisationMethod
{
    private Random _random = new Random();
    private double _c;
    private bool _firstIteration = true;
    
    public (double, double) FindNewInterval(double a, double b, Func<double, double> function)
    {
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
                return (a, u);
            }
            else
            {
                var c = _c;
                _c = u;
                if (u <= c || u >= b)
                    _firstIteration = true;
                return (c, b);
            }
        }
        else
        {
            if (function.Invoke(_c) < function.Invoke(u))
            {
                return (u, b);
            }
            else
            {
                var c = _c;
                _c = u;
                if (u <= a || u >= c)
                    _firstIteration = true;
                return (a, c);
            }
        }
    }
}