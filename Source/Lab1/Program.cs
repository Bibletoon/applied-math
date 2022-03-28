using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab1.Tools;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        List<double> accuracyList = new List<double>(){1e-3, 1e-5, 1e-7};

        var a = -4.5;
        var b = 2.3;
        
        foreach (var accuracy in accuracyList)
        {
            var delta = accuracy / 3;
            var n = 1;

            while (FibonacciCounter.GetNthNumber(n) <= (b-a)/accuracy)
            {
                n++;
            }
            
            var dihMethod = new DichotomyMethod(delta);
            var fibMethod = new FibonacciMethod(n);
            var parabolaMethod = new ParabolaMethod();
            var goldenRatioMethod = new GoldenRatioMethod();
            var brentMethod = new CombinedBrentMethod(accuracy/100);

            var func = (double arg) => Math.Exp(Math.Sin(arg)) * arg * arg;
        
            var dihResult = OptimisationMethodRunner
                .FindFunctionMinimum(new BoundedOptimizationContext(a, b),
                                     accuracy,10, func, dihMethod);
            var fibResult = OptimisationMethodRunner
                .FindFunctionMinimum(new BoundedOptimizationContext(a, b),
                                     accuracy,10, func, fibMethod);
            var parabolaResult = OptimisationMethodRunner
                .FindFunctionMinimum(new ParabolaOptimisationContext(a, b),
                                     accuracy,10, func, parabolaMethod);
            var goldenRatioResult = OptimisationMethodRunner
                .FindFunctionMinimum(new GoldenRatioOptimisationContext(a, b),
                                     accuracy,10, func, goldenRatioMethod);
            var brentResult = OptimisationMethodRunner
                .FindFunctionMinimum(new BrentOptimizationContext(a, b),
                                     accuracy,10, func, brentMethod);

            Console.WriteLine(CreateTable(accuracy, dihResult, brentResult, parabolaResult, goldenRatioResult, fibResult));
            Console.WriteLine();
        }
    }
    
    
    // public static void CreatePlot(
    //     double accuracy,
    //     RunnerResult dichotomyResult,
    //     RunnerResult brentResult,
    //     RunnerResult parabolaResult,
    //     RunnerResult goldenRatioResult)
    // {
    // }
    
    public static string CreateTable(
        double accuracy,
        RunnerResult<BoundedOptimizationContext> dichotomyResult,
        RunnerResult<BrentOptimizationContext> brentResult,
        RunnerResult<ParabolaOptimisationContext> parabolaResult,
        RunnerResult<GoldenRatioOptimisationContext> goldenRatioResult,
        RunnerResult<BoundedOptimizationContext> fibResult
        )
    {
        var table = new []
        {
            new
            {
                AMethod = "Dichotomy", BIterations = dichotomyResult.IterationsCount,
                CFunctionCalss = dichotomyResult.FunctionCallsCount, DResult = dichotomyResult.Result
            },
            new
            {
                AMethod = "Brent", BIterations = brentResult.IterationsCount,
                CFunctionCalss = brentResult.FunctionCallsCount, DResult = brentResult.Result
            },
            new
            {
                AMethod = "Parabola", BIterations = parabolaResult.IterationsCount,
                CFunctionCalss = parabolaResult.FunctionCallsCount, DResult = parabolaResult.Result
            },
            new
            {
                AMethod = "Golden Ratio", BIterations = goldenRatioResult.IterationsCount,
                CFunctionCalss = goldenRatioResult.FunctionCallsCount, DResult = goldenRatioResult.Result
            },
            new
            {
                AMethod = "Fibonacci", BIterations = fibResult.IterationsCount,
                CFunctionCalss = fibResult.FunctionCallsCount, DResult = fibResult.Result
            },
        };

        return table.ToMarkdownTable();
    }
}