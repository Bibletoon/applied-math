using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class OptimizationResult
{
    public Vector<double> Point { get; init; }
    public double Value { get; init; }
    public IReadOnlyCollection<Vector<double>> PointsHistory { get; init; }

    public OptimizationResult(Vector<double> point, double value, IEnumerable<Vector<double>> pointsHistory)
    {
        Point = point;
        Value = value;
        PointsHistory = pointsHistory.ToList();
    }
}