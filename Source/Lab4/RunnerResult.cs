namespace Lab4;

public class RunnerResult
{
    public List<EigenValue> EigenValues { get; }
    public int IterationsCount { get; }

    public RunnerResult(List<EigenValue> eigenValues, int iterationsCount)
    {
        EigenValues = eigenValues;
        IterationsCount = iterationsCount;
    }
}