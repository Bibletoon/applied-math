using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class OptimizationTask
{
    public Vector<double> StartPoint { get; init; }
    public double GradientAccuracy { get; init; }
    public double FunctionAccuracy { get; init; }
    public Func<Vector<double>, double> Function { get; init; }

    public OptimizationTask(Vector<double> startPoint, double gradientAccuracy, double functionAccuracy, Func<Vector<double>, double> function)
    {
        StartPoint = startPoint;
        GradientAccuracy = gradientAccuracy;
        FunctionAccuracy = functionAccuracy;
        Function = function;
    }
}