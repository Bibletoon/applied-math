using Lab2.Models.Functions;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class FunctionOptimizationOptions
{
    private List<Vector<double>> _startPoints;
    public MathFunction Function { get; init; }
    public IReadOnlyCollection<Vector<double>> StartPoints => _startPoints.AsReadOnly();

    public FunctionOptimizationOptions(MathFunction function, IEnumerable<Vector<double>> startPoints)
    {
        Function = function;
        _startPoints = startPoints.ToList();
    }
}