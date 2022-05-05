using Lab3.EquationSystemSolvers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;

namespace Lab3.Tools;

public class InverseMatrixCalculator
{
    private readonly IEquationSystemSolver _equationSystemSolver;

    public InverseMatrixCalculator(IEquationSystemSolver equationSystemSolver)
    {
        _equationSystemSolver = equationSystemSolver;
    }

    public Matrix<double> Calculate(Matrix<double> matrix)
    {
        var n = matrix.RowCount;
        var (l, u) = LuFactorizator.Factorize(matrix);
        Matrix<double> result = new SparseMatrix(n, 0);

        for (var i = 0; i < n; i++)
        {
            var localI = i;
            SparseVectorStorage<double>? sparseE = SparseVectorStorage<double>.OfInit(n, ii => ii == localI ? 1 : 0);
            var vectorE = new SparseVector(sparseE);

            Vector<double> y = _equationSystemSolver.Solve(l, vectorE);
            Vector<double> a = _equationSystemSolver.Solve(u, y);
            result = result.Append(a.ToColumnMatrix());
        }

        return result;
    }
}