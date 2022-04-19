using MathNet.Numerics.LinearAlgebra;

namespace Lab2.Models.Functions;

// TODO: better name
public class ConstMathFunction : MathFunction
{
    private Func<Vector<double>, double> _function;
    private string _stringRepresentation;

    public ConstMathFunction(Func<Vector<double>, double> func, string stringRepresentation)
    {
        _function = func;
        _stringRepresentation = stringRepresentation;
    }

    public override double Invoke(Vector<double> point) => _function.Invoke(point);

    public override string ToString() => _stringRepresentation;
}