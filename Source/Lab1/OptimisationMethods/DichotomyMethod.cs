namespace Lab1.OptimisationMethods;

public class DichotomyMethod : IOptimisationMethod
{
    private double _delta;

    public DichotomyMethod(double delta)
    {
        _delta = delta;
    }

    public (double, double) FindNewInterval(double a, double b, Func<double, double> function)
    {
        var x1 = (a + b) / 2 - _delta;
        var x2 = (a + b) / 2 + _delta;

        if (function.Invoke(x1) < function.Invoke(x2))
        {
            return (a, x2);
        }
        if (function.Invoke(x2) < function.Invoke(x1))
        {
            return (x1, b);
        }

        return (x1, x2);
    }
}