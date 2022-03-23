namespace Lab1.OptimisationMethods;

public class ParabolaMethod : IOptimisationMethod
{
    private Random _random = new Random();
    
    public (decimal, decimal) FindNewInterval(decimal a, decimal b, Func<decimal, decimal> function)
    {
        decimal c = (decimal)_random.NextDouble() * (b - a) + a;

        decimal u = c - (((c - a) * (c - a)) * (function.Invoke(c) - function.Invoke(b)) -
                         ((c - b) * (c - b)) * (function.Invoke(c) - function.Invoke(a))) /
            (2 * ((c - a) * (function.Invoke(c) - function.Invoke(b)) -
                  (c - b) * (function.Invoke(c) - function.Invoke(a))));

        if (c < u)
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return (a, u);
            }
            else
            {
                return (c, b);
            }
        }
        else
        {
            if (function.Invoke(c) < function.Invoke(u))
            {
                return (u, b);
            }
            else
            {
                return (a, c);
            }
        }
    }
}