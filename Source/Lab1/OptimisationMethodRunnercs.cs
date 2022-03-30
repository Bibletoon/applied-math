using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab1.Results;

namespace Lab1;

public static class OptimisationMethodRunner
{
    public static RunResult<TContext> FindFunctionMinimum<TContext>(
        double accuracy,
        TContext context,
        Func<double, double> function,
        IOptimisationMethod<TContext> optimisationMethod,
        int iterationsLimit = int.MaxValue)
        where TContext : IOptimizationContext
    {
        var decimalCount = 1;
        var ac = accuracy;

        while (ac < 1)
        {
            decimalCount++;
            ac *= 10;
        }
        
        var callsCounterFunc = new FunctionCallsCounter<double, double>(
            function,
            i => Math.Round(i, decimalCount));

        int iterationCount = 0;
        var intervalsHistory = new List<TContext> { context };

        while (Math.Abs(context.B - context.A) >= accuracy &&
               iterationCount <= iterationsLimit)
        {
            iterationCount++;
            context = optimisationMethod.FindNewInterval(context, callsCounterFunc.Invoke);
            intervalsHistory.Add(context);
        }

        double result;
        
        result = (context.A + context.B) / 2;

        return new RunResult<TContext>(
        accuracy,
        result,
        callsCounterFunc.CallsCount,
        iterationCount,
        intervalsHistory);
    }
}