namespace Lab1.OptimisationMethods;

public class GoldenRatioMethod : IOptimisationMethod
{
    private static readonly double GoldenRatioProportion = (double)((1+Math.Sqrt(5))/2);

    public (double, double) FindNewInterval(double a, double b, Func<double, double> function)
    {
        var x1 = b - (b - a) / GoldenRatioProportion;
        var x2 = a + (b - a) / GoldenRatioProportion;

        if (function.Invoke(x1) >= function.Invoke(x2))
        {
            return (x1, b);
        }
        else
        {
            return (a, x2);
        }
    }
}