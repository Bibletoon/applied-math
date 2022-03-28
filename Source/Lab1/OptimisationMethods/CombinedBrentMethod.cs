using Lab1.OptimizationContexts;
using static System.Math;

namespace Lab1.OptimisationMethods;

public class CombinedBrentMethod : IOptimisationMethod<BrentOptimizationContext>
{
    private static readonly double K = ((3 - Sqrt(5)) / 2);
    
    public BrentOptimizationContext FindNewInterval(BrentOptimizationContext context, Func<double, double> function)
    {
        var (a, c, x) = (context.A, context.B, context.X);
        var (fa, fc, fu, fx) = (function(a), function(c), function(x), function(x));

        double u;

        if (fa == fc && fa == fu)
        {
            u = x < (c - a) / 2 
                ? x + K * (c - x) 
                : x - K * (x - a);
        }
        else
        {
            var mid = a + (c - a) / 2;
            u = CountU(a, mid, c, fa, function(mid), fc);
        }

        if (!(a <= u && u <= c))
        {
            if (x < (c - a) / 2)
                u = x + K * (c - x);
            else
                u = x - K * (x - a);
        }

        fu = function(u);

        if (fu <= fx)
        {
            if (u >= x)
                a = x;
            else
                c = x;

            return new BrentOptimizationContext(a, c, u, x, context.W);
        }
        else
        {
            if (u >= x)
                c = u;
            else
                a = u;

            if (fu <= function(context.W) || context.W == x)
                return new BrentOptimizationContext(a, c, x, u, context.W);
            else
                return new BrentOptimizationContext(a, c, x, context.W, u);
        }
    }

    private double CountC1(double a, double b, double c, double a1, double b1, double c1)
    {
        var a2 = Pow((double) a, 2);
        var b2 = Pow((double) b, 2);
        var c2 = Pow((double) c, 2);

        return -a * b1 + a * c1 + b * a1 + c * (b2 - a2 - b * c2) / ((b1 - c1) * (a2 - a1 * b1 - a1 * c1 + b1 * c1));
    }

    private double CountC2(double a, double b, double c, double a1, double b1, double c1)
    {
        var a2 = Pow((double) a1, 2);
        var b2 = Pow((double) b1, 2);
        var C1 = CountC1(a, b, c, a1, b1, c1);

        return (b - a - C1 * (b1 - a1)) / (b2 - a2);
    }

    private double CountU(double a, double b, double c, double a1, double b1, double c1)
    {
        return -(CountC1(a, b, c, a1, b1, c1) / 2 * CountC2(a, b, c, a1, b1, c1));
    }

}