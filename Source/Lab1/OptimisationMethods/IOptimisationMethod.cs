using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public interface IOptimisationMethod<TContext> where TContext : IOptimizationContext
{
    public string Title { get; }
    public TContext FindNewInterval(TContext context, Func<double, double> function);
}