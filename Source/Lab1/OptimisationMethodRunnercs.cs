namespace Lab1.OptimisationMethods;

public static class OptimisationMethodRunner
{
    public static RunnerResult FindFunctionMinimum(
        double a,
        double b,
        double accuracy,
        Func<double, double> function,
        IOptimisationMethod optimisationMethod,
        int iterationsLimit = int.MaxValue)
    {
        var callsCounterFunc = new FunctionCallsCounter<double, double>(function);
        int iterationsCount = 0;
        List<(double, double)> intervalsHistory = new List<(double, double)>() { (a, b) };

        while (Math.Abs(b - a) >= accuracy && iterationsCount <= iterationsLimit)
        {
            iterationsCount++;
            (a, b) = optimisationMethod.FindNewInterval(a, b, callsCounterFunc.Invoke);
            intervalsHistory.Add((a, b));
        }

        return new RunnerResult((a + b) / 2, callsCounterFunc.CallsCount, iterationsCount, intervalsHistory);
    }
}