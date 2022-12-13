using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Requests;

public interface IEquationSystemSolverRequest
{
    Matrix<double> Matrix { get; }
    Vector<double> Result { get; }
}