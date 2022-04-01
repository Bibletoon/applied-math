using Lab1.OptimizationContexts;
using Lab1.Tools;
using static System.Math;

namespace Lab1.OptimisationMethods;

public class CombinedBrentMethod : IOptimisationMethod<BrentOptimizationContext>
{
    public string Title => "Combined Brent Method";
    public double EqualityAccuracy { get; set; }

    public BrentOptimizationContext FindNewInterval(BrentOptimizationContext context, Func<double, double> function)
    {
        var (a, b) = (context.A, context.B);
        var (x, w, v) = (context.X, context.W, context.V);
        var (fx, fw, fv) = (function(x), function(w), function(v));

        var g = context.PreviousDistance / 2;
        var previousDistance = context.CurrentDistance;

        var uq = CalculateParabolaVertex(x, w, v, fx, fw, fv);
        double u;

        if (uq is null || !(a <= uq.Value && uq.Value <= b) || Abs(uq.Value - x) > g)
        {
            if (x < (a + b) / 2)
            {
                u = x + Constants.GoldenRatioProportion * (b - x);
                previousDistance = b - x;
            }
            else
            {
                u = x - Constants.GoldenRatioProportion * (x - a);
                previousDistance = x - a;
            }
        }
        else
        {
            u = uq.Value;
        }

        var currentDistance = Abs(u - x);
        var fu = function(u);

        if (fu > fx)
        {
            if (u < x)
                a = u;
            else
                b = u;

            if (fu <= fw || Abs(w - x) < EqualityAccuracy)
                return new BrentOptimizationContext(a, b, x, u, w, currentDistance, previousDistance);

            if (fu <= fv || Abs(v - x) < EqualityAccuracy || Abs(v - w) < EqualityAccuracy)
                return new BrentOptimizationContext(a, b, x, w, u, currentDistance, previousDistance);
        }

        if (u < x)
            b = x;
        else
            a = x;

        return new BrentOptimizationContext(a, b, u, x, w, currentDistance, previousDistance);
    }

    private static double? CalculateParabolaVertex(double a, double b, double c, double fa, double fb, double fc)
    {
        var numerator = Pow(b - a, 2) * (fb - fc) - Pow(b - c, 2) * (fb - fa);
        var denominator = 2 * ((b - a) * (fb - fc) - (b - c) * (fb - fa));

        if (denominator is 0)
            return null;

        return b - numerator / denominator;
    }
}