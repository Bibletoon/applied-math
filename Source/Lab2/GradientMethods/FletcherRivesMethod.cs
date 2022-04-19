using Lab1;
using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientMethods;

public class FletcherRivesMethod<T> : GradientMethod where T : IOptimizationContext
{
    private Vector<double>? _previousAlpha;
    private Vector<double>? _previousPoint;
    private readonly IOptimisationMethod<T> _method;
    private readonly T _context;
    private readonly double _accuracy;

    public FletcherRivesMethod(IOptimisationMethod<T> method, T context, double accuracy)
    {
        _method = method;
        _context = context;
        _accuracy = accuracy;
    }

    public override string Title => "Fletcher-Rives method";

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        Vector<double> alpha;
        var currentGradient = parameters.Function.GradientAt(parameters.Point);
        if (_previousPoint is null)
        {
            alpha = -1 * currentGradient;
        }
        else
        {
            var previousGradient = parameters.Function.GradientAt(_previousPoint);
            double beta = (currentGradient.Norm(currentGradient.Count) * currentGradient.Norm(currentGradient.Count)) 
                          / (previousGradient.Norm(previousGradient.Count) * previousGradient.Norm(previousGradient.Count));

            alpha = -1 * currentGradient + beta * _previousAlpha;
        }

        var functionToMinimize = new Func<double, double>((t) => parameters.Function.Invoke(parameters.Point + t * alpha));

        var result = OptimisationMethodRunner.FindFunctionMinimum(_accuracy, _context, functionToMinimize, _method);
        
        _previousAlpha = alpha;
        _previousPoint = parameters.Point;

        return parameters.Point + result.Result * alpha;
    }
}