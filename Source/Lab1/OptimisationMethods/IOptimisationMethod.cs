namespace Lab1.OptimisationMethods;

public interface IOptimisationMethod
{
    public (double,double) FindNewInterval(double a, double b, Func<double, double> function);
}