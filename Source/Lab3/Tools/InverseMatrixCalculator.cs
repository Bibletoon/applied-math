using Lab3.EquationSystemSolvers.Requests;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;
using GenericSolver = Lab3.EquationSystemSolvers.IEquationSystemSolver<
    Lab3.EquationSystemSolvers.Requests.IEquationSystemSolverRequest,
    Lab3.EquationSystemSolvers.Responses.IEquationSystemSolverResponse>;

namespace Lab3.Tools;

public class InverseMatrixCalculator
{
    private readonly GenericSolver _equationSystemSolver;

    public InverseMatrixCalculator(GenericSolver equationSystemSolver)
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

            Vector<double> y = _equationSystemSolver.Solve(new SimpleEquationSystemSolverRequest(l, vectorE)).Solution;
            Vector<double> a = _equationSystemSolver.Solve(new SimpleEquationSystemSolverRequest(u, y)).Solution;
            result = result.Append(a.ToColumnMatrix());
        }

        return result;
    }
}