using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public static class OptimisationMethodRunner
{
    public static RunnerResult<TContext> FindFunctionMinimum<TContext>(
        TContext context,
        double accuracy,
        Func<double, double> function,
        IOptimisationMethod<TContext> optimisationMethod,
        int iterationsLimit = int.MaxValue) where TContext : IOptimizationContext
    {
        var callsCounterFunc = new FunctionCallsCounter<double, double>(function);
        int iterationsCount = 0;
        List<TContext> intervalsHistory = new List<TContext>() { context };

        while (Math.Abs(context.B - context.A) >= accuracy && 
               Math.Abs(context.A) >= accuracy && 
               Math.Abs(context.B) >= accuracy && 
               iterationsCount <= iterationsLimit)
        {
            iterationsCount++;
            context = optimisationMethod.FindNewInterval(context, callsCounterFunc.Invoke);
            intervalsHistory.Add(context);
        }
        
        double result;
        
        if (Math.Abs(context.B - context.A) < accuracy)
        {
            result = (context.A + context.B) / 2;
        } else if (Math.Abs(context.A) < accuracy)
        {
            result = context.A;
        }
        else
        {
            result = context.B;
        }

        return new RunnerResult<TContext>(
            result,
            callsCounterFunc.CallsCount, 
            iterationsCount, 
            intervalsHistory);
    }
}