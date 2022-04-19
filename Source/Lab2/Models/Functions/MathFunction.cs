using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab2.Models.Functions;

public abstract class MathFunction
{
    private const double Epsilon = 1e-6;
    private static readonly Dictionary<Vector<double>, double> FunctionCache = new Dictionary<Vector<double>, double>();
    private static readonly Dictionary<Vector<double>, Vector<double>> GradientCache = new Dictionary<Vector<double>, Vector<double>>();

    protected abstract double GetValue(Vector<double> point);
    public abstract override string ToString();

    public double Invoke(Vector<double> point)
    {
        if (FunctionCache.ContainsKey(point))
            return FunctionCache[point];

        var value = GetValue(point);
        FunctionCache[point] = value;
        return value;
    }

    public Vector<double> GradientAt(Vector<double> point)
    {
        if (GradientCache.ContainsKey(point))
            return GradientCache[point];
        
        var coordinates = new List<double>();

        for (var i = 0; i < point.Count; i++)
        {
            var nextPoint = point.Clone();
            nextPoint[i] += Epsilon;
            coordinates.Add((Invoke(nextPoint) - Invoke(point))/Epsilon);
        }

        var gradient = DenseVector.OfEnumerable(coordinates);
        GradientCache[point] = gradient;
        return gradient;
    }
}