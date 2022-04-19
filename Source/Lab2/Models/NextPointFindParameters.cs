using Lab2.Models.Functions;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models;

public class NextPointFindParameters
{
    public MathFunction Function { get; init; }
    public Vector<double> Point { get; init; }

    public NextPointFindParameters(MathFunction function, Vector<double> point)
    {
        Function = function;
        Point = point;
    }
}