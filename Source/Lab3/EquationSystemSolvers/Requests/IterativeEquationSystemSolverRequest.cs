using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Requests;

public record struct IterativeEquationSystemSolverRequest(
    Matrix<double> Matrix,
    Vector<double> InitialApproximation,
    Vector<double> Result,
    int MaxIterationCount,
    double Accuracy) : IEquationSystemSolverRequest;