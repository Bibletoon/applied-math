using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models.Functions;

public abstract class MathFunction 
{
    public abstract double Invoke(Vector<double> point);
    public abstract override string ToString();
}