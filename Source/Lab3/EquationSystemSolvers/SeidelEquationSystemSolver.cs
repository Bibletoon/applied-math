using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;
using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers;

public class SeidelEquationSystemSolver : IEquationSystemSolver<
    IterativeEquationSystemSolverRequest, IterativeEquationSystemSolverResponse>
{
    public string Name => "Seidel method";

    public IterativeEquationSystemSolverResponse Solve(IterativeEquationSystemSolverRequest request)
    {
        var (matrix, result, maxIterationCount, accuracy) = request;

        var n = matrix.RowCount;
        var iterationCount = 0;

        Vector<double> x = VectorPool<double>.Get(n, i => result[i] / matrix[i, i]);
        Vector<double> tempX = VectorPool<double>.Get(n);
        double norm;

        do
        {
            for (var i = 0; i < n; i++)
            {
                tempX[i] = result[i];

                for (var j = 0; j < i; j++)
                {
                    tempX[i] -= matrix[i, j] * tempX[j];
                }

                for (var j = i + 1; j < n; j++)
                {
                    tempX[i] -= matrix[i, j] * x[j];
                }

                tempX[i] /= matrix[i, i];
            }

            iterationCount++;

            norm = CountNorm(x, tempX);
            tempX.CopyTo(x);
        }
        while (norm > accuracy && iterationCount < maxIterationCount);

        VectorPool<double>.Return(tempX);

        return new IterativeEquationSystemSolverResponse(x, iterationCount);
    }

    private static double CountNorm(Vector<double> first, Vector<double> second)
    {
        var sum = first.Zip(second)
            .Select(p => Math.Pow(p.First - p.Second, 2))
            .Sum();

        return Math.Sqrt(sum);
    }
}