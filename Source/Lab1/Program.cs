using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        var acc = 1m / 100;
        var delta = 0.003m;

        var method = new DichotomyMethod(delta);
        var context = new BoundedOptimizationContext(-16m, 16m);

        var res = OptimisationMethodRunner
            .FindFunctionMinimum(
                context,
                acc,
                arg => (decimal) Math.Exp(Math.Sin((double) arg)) * arg * arg,
                method);

        var interval = res.Intervals[^1];
        var point = interval.A + (interval.B - interval.A) / 2;

        Console.WriteLine(point);
    }
}