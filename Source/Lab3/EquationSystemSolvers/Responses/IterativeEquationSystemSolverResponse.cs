using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Responses;

public record struct IterativeEquationSystemSolverResponse(
    Vector<double> Solution,
    IReadOnlyCollection<Vector<double>> SolutionHistory) : IEquationSystemSolverResponse;