using Lab1.OptimizationContexts;
using Lab1.Tools;

namespace Lab1.OptimisationMethods;

public class FibonacciMethod : IOptimisationMethod<FibonacciOptimizationContext>
{
    public string Title => "Fibonacci Method";
    public int N { get; set; }

    public FibonacciOptimizationContext FindNewInterval(FibonacciOptimizationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);
        var (px1, px2) = (context.X1, context.X2);

        var x1 = px1 ?? a + (FibonacciCounter.GetNthNumber(N - 2) / FibonacciCounter.GetNthNumber(N)) * (b - a);
        var x2 = px2 ?? a + (FibonacciCounter.GetNthNumber(N - 1) / FibonacciCounter.GetNthNumber(N)) * (b - a);

        if (function.Invoke(x1) > function.Invoke(x2))
        {
            px1 = x2;
            px2 = b - (px1 - x1);
            return new FibonacciOptimizationContext(x1, b, px1, px2);
        }
        else
        {
            px2 = x1;
            px1 = a + (x2 - px2);
            return new FibonacciOptimizationContext(a, x2, px1, px2);
        }
    }
}