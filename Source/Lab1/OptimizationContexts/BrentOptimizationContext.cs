namespace Lab1.OptimizationContexts;

public class BrentOptimizationContext : IOptimizationContext
{
    public BrentOptimizationContext(double a, double b)
    {
        A = a;
        B = b;
        X = W = V = a + (b - a) / 2;
    }
    
    public BrentOptimizationContext(double a, double b, double x, double w, double v)
    {
        A = a;
        B = b;
        X = x;
        W = w;
        V = v;
    }

    public double A { get; }
    public double B { get; }
    
    // Точка, соответсвующая наименьшиму значение функции
    public double X { get; }
    
    // Точка, соответсвующая второму снизу значению функции
    public double W { get; }
    
    // Предыдущее значение W
    public double V { get; }
}