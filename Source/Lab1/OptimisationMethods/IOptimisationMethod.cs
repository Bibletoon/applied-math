namespace Lab1.OptimisationMethods;

public interface IOptimisationMethod
{
    public (decimal,decimal) FindNewInterval(decimal a, decimal b, Func<decimal, decimal> function);
}