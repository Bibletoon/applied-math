using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers.Responses;

public interface IEquationSystemSolverResponse
{
    Vector<double> Solution { get; }
}