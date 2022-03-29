using Lab1.OptimizationContexts;

namespace Lab1.Results;

public record RunResult<T>(
    double Accuracy,
    double Result,
    int FunctionCallCount,
    int IterationCount,
    IReadOnlyList<T> Intervals) where T : IOptimizationContext;