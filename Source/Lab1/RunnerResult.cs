namespace Lab1;

public record RunnerResult(decimal result, int functionCallsCount, int iterationsCount, List<(decimal, decimal)> intervals);