using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers;

public interface IEquationSystemSolver
{
    Vector<double> Solve(Matrix<double> matrix, Vector<double> result);
}