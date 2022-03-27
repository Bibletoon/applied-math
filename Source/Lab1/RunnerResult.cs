namespace Lab1;

public record RunnerResult(double result, int functionCallsCount, int iterationsCount, List<(double, double)> intervals);