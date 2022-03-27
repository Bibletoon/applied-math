using Lab1.OptimisationMethods;
using Lab1.Tools;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        double acc = 1e-5;

        double a = -4;
        double b = 2;

        var brent = new BrentMethod(acc);
        var parabola = new ParabolaMethod();
        var gold = new GoldenRatioMethod();
        
        var func = (double x) => (Math.Pow(Math.E,Math.Sin(x))*x*x);
        
        var brentRes = OptimisationMethodRunner.FindFunctionMinimum(a, b, acc, func, brent);
        var parabolaRes = OptimisationMethodRunner.FindFunctionMinimum(a, b, acc, func, parabola);
        var goldRes = OptimisationMethodRunner.FindFunctionMinimum(a, b, acc, func, gold);
    }
}