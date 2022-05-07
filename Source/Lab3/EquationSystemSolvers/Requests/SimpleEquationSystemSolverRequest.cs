using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Requests;

public record struct SimpleEquationSystemSolverRequest(Matrix<double> Matrix, Vector<double> Result)
    : IEquationSystemSolverRequest;