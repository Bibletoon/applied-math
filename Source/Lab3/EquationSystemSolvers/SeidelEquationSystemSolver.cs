using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab3.EquationSystemSolvers;

public class SeidelEquationSystemSolver : IEquationSystemSolver<IterativeEquationSystemSolverRequest, IterativeEquationSystemSolverResponse>
{
    public string Name => $"Seidel method";
    public IterativeEquationSystemSolverResponse Solve(IterativeEquationSystemSolverRequest request)
    {
        var (matrix, result, maxIterationCount, accuracy) = request;
        
        var n = matrix.RowCount;
        var history = new List<Vector<double>>();
        
        var x = new DenseVector(new double[n]);
        var tempX = new DenseVector(new double[n]);
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

            var log = new DenseVector(result.Count);
            x.CopyTo(log);
            history.Add(log);

            norm = CountNorm(x, tempX);
            tempX.CopyTo(x);
        }
        while (norm > accuracy && history.Count < maxIterationCount);

        return new IterativeEquationSystemSolverResponse(x, history);
    }

    private static double CountNorm(Vector<double> first, Vector<double> second)
    {
        var sum =  first.Zip(second)
            .Select(p => Math.Pow(p.First - p.Second, 2))
            .Sum();

        return Math.Sqrt(sum);
    }
}