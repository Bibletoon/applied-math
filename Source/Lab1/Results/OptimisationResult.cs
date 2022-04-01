using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;

namespace Lab1.Results;

public record OptimisationResult<T>(
    IOptimisationMethod<T> OptimisationMethod,
    IReadOnlyList<RunResult<T>> RunResults) where T : IOptimizationContext;