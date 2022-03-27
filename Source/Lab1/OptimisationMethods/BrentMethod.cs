namespace Lab1.OptimisationMethods;

public class BrentMethod : IOptimisationMethod
{
    private static readonly double GoldenRatioProportion = (3-Math.Sqrt(5))/2;
    private Random _random = new Random();
    private double _accuracy;
    private double x;
    private double w;
    private double v;
    private double d;
    private double e;
    private bool firstIteration = true;

    public BrentMethod(double accuracy)
    {
        _accuracy = accuracy;
    }
    
    public (double, double) FindNewInterval(double a, double b, Func<double, double> function)
    {
        if (firstIteration)
        {
            x = w = v = (a + b) / 2;
            d = e = b - a;
            firstIteration = false;
        }

        var g = e;
        e = d;
        double u;

        if (x != w && x != v && w != v
            && function.Invoke(x) != function.Invoke(w)
            && function.Invoke(x) != function.Invoke(v)
            && function.Invoke(w) != function.Invoke(v))
        {
            u = w - (((w - x) * (w - x)) * (function.Invoke(w) - function.Invoke(v)) -
                             ((w - v) * (w - v)) * (function.Invoke(w) - function.Invoke(x))) /
                (2 * ((w - x) * (function.Invoke(w) - function.Invoke(v)) -
                      (w - v) * (function.Invoke(w) - function.Invoke(x))));

            if (a + _accuracy <= u && u <= b - _accuracy && Math.Abs(u-x) < g/2)
            {
                d = Math.Abs(u - x);
            }
            else
            {
                if (x < (a + b) / 2)
                {
                    u = x + GoldenRatioProportion * (b - x);
                    d = b - x;
                }
                else
                {
                    u = x - GoldenRatioProportion * (x - a);
                    d = x - a;
                }
            }
        }
        else
        {
            if (x < (a + b) / 2)
            {
                u = x + GoldenRatioProportion * (b - x);
                d = b - x;
            }
            else
            {
                u = x - GoldenRatioProportion * (x - a);
                d = x - a;
            }
        }

        if (function.Invoke(u) <= function.Invoke(x))
        {
            if (u >= x)
            {
                a = x;
            }
            else
            {
                b = x;
            }

            v = w;
            w = x;
            x = u;
        }
        else
        {
            if (u >= x)
            {
                b = u;
            }
            else
            {
                a = u;
            }

            if (function.Invoke(u) <= function.Invoke(w) || w == x)
            {
                v = w;
                w = u;
            } else if (function.Invoke(u) <= function.Invoke(v) || v == x || v == w)
            {
                v = u;
            }
        }

        return (a, b);
    }
}