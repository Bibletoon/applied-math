using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;

namespace Lab3.EquationSystemSolvers;

public interface IEquationSystemSolver<in TRequest, out TResponse>
    where TRequest : IEquationSystemSolverRequest
    where TResponse : IEquationSystemSolverResponse
{
    string Name { get; }
    TResponse Solve(TRequest request);
}