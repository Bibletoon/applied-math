using Lab1.OptimizationContexts;

namespace Lab1.OptimisationMethods;

public interface IOptimisationMethod<TContext> where TContext : IOptimizationContext
{
    public TContext FindNewInterval(TContext context, Func<decimal, decimal> function);
}