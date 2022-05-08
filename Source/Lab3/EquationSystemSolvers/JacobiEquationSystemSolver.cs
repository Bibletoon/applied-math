using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;
using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab3.EquationSystemSolvers;

public class JacobiEquationSystemSolver : IEquationSystemSolver<
    IterativeEquationSystemSolverRequest, IterativeEquationSystemSolverResponse>
{
    public string Name => "Jacobi method";

    public IterativeEquationSystemSolverResponse Solve(IterativeEquationSystemSolverRequest request)
    {
        var (matrix, result, maxIterationCount, accuracy) = request;

        var n = result.Count;
        var iterationCount = 0;

        Vector<double> x = VectorPool<double>.Get(n);
        Vector<double> tempX = VectorPool<double>.Get(n);
        double norm;

        do
        {
            for (var i = 0; i < n; i++)
            {
                tempX[i] = result[i];

                for (var j = 0; j < n; j++)
                {
                    if (i == j)
                        continue;

                    tempX[i] -= matrix[i, j] * x[j];
                }

                tempX[i] /= matrix[i, i];
            }

            iterationCount++;

            norm = 0;
            for (var i = 0; i < n; i++)
            {
                norm = Math.Max(norm, Math.Abs(x[i] - tempX[i]));
            }

            tempX.CopyTo(x);
        }
        while (norm > accuracy && iterationCount < maxIterationCount);

        VectorPool<double>.Return(tempX);

        return new IterativeEquationSystemSolverResponse(x, iterationCount);
    }
}