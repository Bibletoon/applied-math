using Lab2.Models.Functions;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class OptimizationRequest
{
    public Vector<double> StartPoint { get; init; }
    public double GradientAccuracy { get; init; }
    public double FunctionAccuracy { get; init; }
    public MathFunction Function { get; init; }

    public OptimizationRequest(Vector<double> startPoint, double gradientAccuracy, double functionAccuracy, MathFunction function)
    {
        StartPoint = startPoint;
        GradientAccuracy = gradientAccuracy;
        FunctionAccuracy = functionAccuracy;
        Function = function;
    }
}