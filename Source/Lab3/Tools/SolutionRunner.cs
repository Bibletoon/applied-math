using Lab3.EquationSystemSolvers.Requests;
using Lab3.MatrixGenerators;
using Lab3.Models;
using MathNet.Numerics.LinearAlgebra;
using Solver = Lab3.EquationSystemSolvers.IEquationSystemSolver<
    Lab3.EquationSystemSolvers.Requests.IterativeEquationSystemSolverRequest,
    Lab3.EquationSystemSolvers.Responses.IterativeEquationSystemSolverResponse>;

namespace Lab3.Tools;

public class SolutionRunner
{
    private readonly Solver _equationSystemSolver;
    private readonly ISquareMatrixGenerator<double> _matrixGenerator;
    private readonly int _maxIterationCount;

    public SolutionRunner(
        Solver equationSystemSolver,
        ISquareMatrixGenerator<double> matrixGenerator,
        int maxIterationCount)
    {
        _equationSystemSolver = equationSystemSolver;
        _maxIterationCount = maxIterationCount;
        _matrixGenerator = matrixGenerator;
    }

    public SolutionRunnerResult Run(int n, int k, double accuracy, Vector<double> initialApproximation)
    {
        Matrix<double> a = _matrixGenerator.Generate(n, k);
        Vector<double> x = VectorPool<double>.Get(a.ColumnCount, i => i + 1);
        Vector<double> f = VectorPool<double>.Get(x.Count);
        a.Multiply(x, f);

        Console.WriteLine($"Solving system with {n}x{n} and k = {k} {_matrixGenerator.Name}, using {_equationSystemSolver.Name}");

        var request = new IterativeEquationSystemSolverRequest(a, initialApproximation, f, _maxIterationCount, accuracy);
        var response = _equationSystemSolver.Solve(request);
        
        MatrixPool<double>.Return(a);
        VectorPool<double>.Return(f);

        return new SolutionRunnerResult(
            MethodTitle: _equationSystemSolver.Name,
            MatrixTitle: _matrixGenerator.Name,
            InitialApproximation: initialApproximation,
            Actual: x,
            Received: response.Solution,
            Accuracy: accuracy,
            K: k,
            IterationCount: response.IterationCount);
    }
}