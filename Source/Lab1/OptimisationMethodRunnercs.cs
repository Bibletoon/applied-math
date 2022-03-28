using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public static class OptimisationMethodRunner
{
    public static RunnerResult<TContext> FindFunctionMinimum<TContext>(
        TContext context,
        decimal accuracy,
        Func<decimal, decimal> function,
        IOptimisationMethod<TContext> optimisationMethod,
        int iterationsLimit = int.MaxValue) where TContext : IOptimizationContext
    {
        var callsCounterFunc = new FunctionCallsCounter<decimal, decimal>(function);
        int iterationsCount = 0;
        List<TContext> intervalsHistory = new List<TContext>() { context };

        while (Math.Abs(context.B - context.A) >= accuracy && iterationsCount <= iterationsLimit)
        {
            iterationsCount++;
            context = optimisationMethod.FindNewInterval(context, callsCounterFunc.Invoke);
            intervalsHistory.Add(context);
        }

        return new RunnerResult<TContext>(
            (context.A + context.B) / 2,
            callsCounterFunc.CallsCount, 
            iterationsCount, 
            intervalsHistory);
    }
}