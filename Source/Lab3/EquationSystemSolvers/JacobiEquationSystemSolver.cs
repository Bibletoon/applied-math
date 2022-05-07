using Lab3.EquationSystemSolvers.Requests;
using Lab3.EquationSystemSolvers.Responses;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab3.EquationSystemSolvers;

public class JacobiEquationSystemSolver : 
    IEquationSystemSolver<IterativeEquationSystemSolverRequest, IterativeEquationSystemSolverResponse>
{
    public string Name => "Jacobi method";
    public IterativeEquationSystemSolverResponse Solve(IterativeEquationSystemSolverRequest request)
    {
        var (matrix, result, maxIterationCount, accuracy) = request;
        
        var n = result.Count;
        var history = new List<Vector<double>>();
        
        var x = new DenseVector(new double[n]);
        var tempX = new DenseVector(new double[n]);
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

            var log = new DenseVector(result.Count);
            x.CopyTo(log);
            history.Add(log);
            
            norm = Math.Abs(x[0] - tempX[0]);

            for (var i = 1; i < n; i++)
            {
                var tempNorm = Math.Abs(x[i] - tempX[i]);
                
                if (tempNorm > norm)
                    norm = tempNorm;
            }
        }
        while (norm > accuracy && history.Count < maxIterationCount);

        return new IterativeEquationSystemSolverResponse(x, history);
    }
}