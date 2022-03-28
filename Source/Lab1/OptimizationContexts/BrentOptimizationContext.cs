namespace Lab1.OptimizationContexts;

public class BrentOptimizationContext : IOptimizationContext
{
    public BrentOptimizationContext(decimal a, decimal b)
    {
        A = a;
        B = b;
        X = W = V = a + (b - a) / 2;
    }
    
    public BrentOptimizationContext(decimal a, decimal b, decimal x, decimal w, decimal v)
    {
        A = a;
        B = b;
        X = x;
        W = w;
        V = v;
    }

    public decimal A { get; }
    public decimal B { get; }
    
    // Точка, соответсвующая наименьшиму значение функции
    public decimal X { get; }
    
    // Точка, соответсвующая второму снизу значению функции
    public decimal W { get; }
    
    // Предыдущее значение W
    public decimal V { get; }
}