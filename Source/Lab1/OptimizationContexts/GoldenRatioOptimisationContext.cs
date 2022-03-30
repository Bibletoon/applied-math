namespace Lab1.OptimizationContexts;

public record GoldenRatioOptimisationContext(double A, double B, double? X1 = null, double? X2 = null) : IOptimizationContext;