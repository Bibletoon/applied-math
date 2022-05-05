using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab3.EquationSystemSolvers;

public class GaussianEquationSystemSolver : IEquationSystemSolver
{
    public Vector<double> Solve(Matrix<double> matrix, Vector<double> result)
    {
        var n = result.Count;
        Matrix<double> extendedMatrix = matrix.Append(result.ToColumnMatrix());

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i == j)
                    continue;

                var multiplier = extendedMatrix[j, i] / extendedMatrix[i, i];

                for (var k = 0; k <= n; k++)
                {
                    extendedMatrix[j, k] -= multiplier * extendedMatrix[i, k];
                }
            }
        }

        var solution = new DenseVector(n);

        for (var i = 0; i < n; i++)
        {
            solution[i] = extendedMatrix[i, n] / extendedMatrix[i, i];
        }

        return solution;
    }
}