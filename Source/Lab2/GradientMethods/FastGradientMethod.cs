using Lab1;
using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientMethods;

public class FastGradientMethod<T> : GradientMethod where T : IOptimizationContext
{
    private readonly IOptimisationMethod<T> _method;
    private readonly T _context;
    private readonly double _accuracy;

    public FastGradientMethod(IOptimisationMethod<T> method, T context, double accuracy)
    {
        _method = method;
        _context = context;
        _accuracy = accuracy;
    }

    public override string Title => $"Fast Gradient Descent Method";
    public override string FullTitle => $"Fast GDM with {_method.Title}";

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        var functionForMinimization = new Func<double, double>((x) => parameters.Function.Invoke(parameters.Point - x * parameters.Function.GradientAt(parameters.Point)));

        var result = OptimisationMethodRunner.FindFunctionMinimum(_accuracy, _context, functionForMinimization, _method);

        return parameters.Point - result.Result * parameters.Function.GradientAt(parameters.Point);
    }
}