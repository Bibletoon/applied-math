using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Responses;

public record struct IterativeEquationSystemSolverResponse(
    Vector<double> Solution, int IterationCount) : IEquationSystemSolverResponse;