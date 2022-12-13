using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;

namespace Lab3.EquationSystemSolvers;

public class GaussianIterativeWrapper : IEquationSystemSolver<
    IterativeEquationSystemSolverRequest,
    IterativeEquationSystemSolverResponse>
{
    private readonly GaussianEquationSystemSolver _gaussianEquationSystemSolver;

    public GaussianIterativeWrapper(GaussianEquationSystemSolver gaussianEquationSystemSolver)
    {
        _gaussianEquationSystemSolver = gaussianEquationSystemSolver;
    }

    public string Name => _gaussianEquationSystemSolver.Name;

    public IterativeEquationSystemSolverResponse Solve(IterativeEquationSystemSolverRequest request)
    {
        var response = _gaussianEquationSystemSolver.Solve(request);
        return new IterativeEquationSystemSolverResponse(response.Solution, (int)Math.Pow(response.Solution.Count, 3));
    }
}