using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab2.Tools;

public static class Extensions
{
    private const double Epsilon = 1e-6;

    public static Vector<double> GradientAt(this Func<Vector<double>, double> func, Vector<double> point)
    {
        var coordinates = new List<double>();

        for (var i = 0; i < point.Count; i++)
        {
            var nextPoint = point.Clone();
            nextPoint[i] += Epsilon;
            coordinates.Add((func.Invoke(nextPoint) - func.Invoke(point))/Epsilon);
        }

        return DenseVector.OfEnumerable(coordinates);
    }
}