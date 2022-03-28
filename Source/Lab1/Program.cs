using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        double acc = 1e-2;
        var delta = 0.003d;

        var method = new CombinedBrentMethod(acc);
        var context = new BrentOptimizationContext(-16, 16);

        var res = OptimisationMethodRunner
            .FindFunctionMinimum(
                context,
                acc,
                10,
                arg => Math.Exp(Math.Sin(arg)) * arg * arg,
                method);
        
        Console.WriteLine(res);
    }
}