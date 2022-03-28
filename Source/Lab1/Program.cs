using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        double acc = 1e-5;
        var delta = 0.003d;

        var method = new DichotomyMethod(delta);
        var context = new BoundedOptimizationContext(-16, 16);

        var res = OptimisationMethodRunner
            .FindFunctionMinimum(
                context,
                acc,
                arg => Math.Exp(Math.Sin(arg)) * arg * arg,
                method);

        var interval = res.Intervals[^1];
        var point = interval.A + (interval.B - interval.A) / 2;

        Console.WriteLine(point);
    }
}