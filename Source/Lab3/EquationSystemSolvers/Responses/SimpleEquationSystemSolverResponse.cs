using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Responses;

public record struct SimpleEquationSystemSolverResponse(Vector<double> Solution) : IEquationSystemSolverResponse;