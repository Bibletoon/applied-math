using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class NextPointFindParameters
{
    public Func<Vector<double>, double> Function { get; init; }
    public Vector<double> Point { get; init; }
    public Vector<double> Gradient { get; init; }

    public NextPointFindParameters(Func<Vector<double>, double> function, Vector<double> point, Vector<double> gradient)
    {
        Function = function;
        Point = point;
        Gradient = gradient;
    }
}