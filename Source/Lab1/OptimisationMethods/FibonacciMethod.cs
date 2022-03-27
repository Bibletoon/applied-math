using Lab1.Tools;

namespace Lab1.OptimisationMethods;

public class FibonacciMethod : IOptimisationMethod
{
    private readonly int _n;
    private double? _x1;
    private double? _x2;

    public FibonacciMethod(int n)
    {
        _n = n;
    }

    public (double, double) FindNewInterval(double a, double b, Func<double, double> function)
    {
        var x1 = _x1 ?? a + (FibonacciCounter.GetNthNumber(_n - 2) / FibonacciCounter.GetNthNumber(_n)) * (b-a);
        var x2 = _x2 ?? a + (FibonacciCounter.GetNthNumber(_n - 1) / FibonacciCounter.GetNthNumber(_n)) * (b-a);

        if (function.Invoke(x1) > function.Invoke(x2))
        {
            _x1 = x2;
            _x2 = b - (_x1 - x1);
            return (x1, b);
        }
        else
        {
            _x2 = x1;
            _x1 = a + (x2 - _x2);
            return (a, x2);
        }
    }
}