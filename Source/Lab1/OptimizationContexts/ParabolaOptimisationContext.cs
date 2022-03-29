namespace Lab1.OptimizationContexts;

public record ParabolaOptimisationContext(double A, double B, double? C = null, double? U = null) : IOptimizationContext;