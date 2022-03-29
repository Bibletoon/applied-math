namespace Lab1.OptimizationContexts;

public interface IOptimizationContext
{
    double A { get; }
    double B { get; }
    bool ShouldExit { get; }
}