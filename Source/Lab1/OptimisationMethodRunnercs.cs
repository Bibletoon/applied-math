namespace Lab1.OptimisationMethods;

public static class OptimisationMethodRunner
{
    public static RunnerResult FindFunctionMinimum(
        decimal a,
        decimal b,
        decimal accuracy,
        Func<decimal, decimal> function,
        IOptimisationMethod optimisationMethod,
        int iterationsLimit = int.MaxValue)
    {
        var callsCounterFunc = new FunctionCallsCounter<decimal, decimal>(function);
        int iterationsCount = 0;
        List<(decimal, decimal)> intervalsHistory = new List<(decimal, decimal)>() { (a, b) };

        while (Math.Abs(b - a) >= accuracy && iterationsCount <= iterationsLimit)
        {
            iterationsCount++;
            (a, b) = optimisationMethod.FindNewInterval(a, b, callsCounterFunc.Invoke);
            intervalsHistory.Add((a, b));
        }

        return new RunnerResult((a + b) / 2, callsCounterFunc.CallsCount, iterationsCount, intervalsHistory);
    }
}