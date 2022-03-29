using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab1.Results;
using Lab1.Tools;
using static System.Math;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        var accuracyList = new[] { 1e-3, 1e-5, 1e-7 };

        var a = -4.5;
        var b = 2.3;

        var dihMethod = new DichotomyMethod();
        var fibMethod = new FibonacciMethod();
        var parabolaMethod = new ParabolaMethod();
        var goldenRatioMethod = new GoldenRatioMethod();
        var brentMethod = new CombinedBrentMethod();

        // var func = (double arg) => Exp(Sin(arg + 1)) * Pow(arg + 1, 2);
        var func = (double x) => Sin(x) - Log(Pow(x, 2)) - 1;
        var spreadsheetGenerator = new SpreadsheetGenerator();

        var dihContext = new BoundedOptimizationContext(a, b);
        RunMethod(accuracyList, func, dihMethod, dihContext, spreadsheetGenerator, (m, acc) => m.Delta = acc / 3);

        var fibContext = new FibonacciOptimizationContext(a, b);
        RunMethod(accuracyList, func, fibMethod, fibContext, spreadsheetGenerator, (m, acc) => m.N = GetN(a, b, acc));

        var parabolaContext = new ParabolaOptimisationContext(a, b);
        RunMethod(accuracyList, func, parabolaMethod, parabolaContext, spreadsheetGenerator);

        var goldenRatioContext = new GoldenRatioOptimisationContext(a, b);
        RunMethod(accuracyList, func, goldenRatioMethod, goldenRatioContext, spreadsheetGenerator);

        var brentContext = new BrentOptimizationContext(a, b);
        RunMethod(accuracyList, func, brentMethod, brentContext, spreadsheetGenerator,
            (m, acc) => m.EqualityAccuracy = acc / 100);

        var fileName = "Lab1.xlsx";
        spreadsheetGenerator.Build(fileName);
    }

    private static void RunMethod<TMethod, TContext>(
        IReadOnlyCollection<double> accuracies,
        Func<double, double> func,
        TMethod method,
        TContext context,
        SpreadsheetGenerator spreadsheetGenerator,
        Action<TMethod, double>? modifier = null)
        where TMethod : IOptimisationMethod<TContext>
        where TContext : IOptimizationContext
    {
        var results = new List<RunResult<TContext>>();

        foreach (double accuracy in accuracies)
        {
            modifier?.Invoke(method, accuracy);
            var result = OptimisationMethodRunner.FindFunctionMinimum(
                accuracy,
                context,
                func,
                method);

            results.Add(result);
        }

        var optimizationResult = new OptimisationResult<TContext>(method, results);
        spreadsheetGenerator.AddOptimizationResultToSpreadsheet(optimizationResult);
    }


    private static int GetN(double a, double b, double accuracy)
    {
        var n = 1;

        while (FibonacciCounter.GetNthNumber(n) <= (b - a) / accuracy)
        {
            n++;
        }

        return n;
    }
}