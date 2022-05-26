using MathNet.Numerics.LinearAlgebra;

namespace Lab4;

public class EigenValue
{
    public double Number { get; }
    public Vector<double> Vector { get; }

    public EigenValue(double number, Vector<double> vector)
    {
        Number = number;
        Vector = vector;
    }
}