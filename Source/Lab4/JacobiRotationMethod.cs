using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab4;

public class JacobiRotationMethod
{
    public RunnerResult GetEigenvalues(Matrix<double> matrix)
    {
        var iterationsCount = 0;
        var (i, j) = (-1, -1);
        Matrix<double> tMatrix = new DenseMatrix(matrix.RowCount);
        do
        {
            var rotationMatrix = GetRotationMatrix(matrix);
            matrix = rotationMatrix.Transpose().Multiply(matrix).Multiply(rotationMatrix);
            if (i == -1 && j == -1)
            {
                rotationMatrix.CopyTo(tMatrix);
            }
            else
            {
                tMatrix = tMatrix.Multiply(rotationMatrix);
            }
            (i, j) = GetMaxIndex(matrix);
            iterationsCount++;
        } while (Math.Abs(matrix[i, j]) > 1e-5 && iterationsCount < 1e+5);

        var values = new List<EigenValue>();
        
        for (int k = 0; k < matrix.RowCount; k++)
        {
            values.Add(new EigenValue(matrix[k,k], tMatrix.Column(k)));
        }

        return new RunnerResult(values, iterationsCount);
    }

    private Matrix<double> GetRotationMatrix(Matrix<double> matrix)
    {
        int n = matrix.ColumnCount;

        var (indexOne, indexTwo) = GetMaxIndex(matrix);

        var cos = GetCos(indexOne, indexTwo, matrix);
        var sin = GetSin(indexOne, indexTwo, matrix);

        var rotationMatrix = MatrixPool<double>.Get(n,n);
        rotationMatrix.Clear();
        for (int i = 0; i < n; i++)
        {
            if (i == indexOne || i == indexTwo)
            {
                rotationMatrix[i, i] = cos;
            }
            else
            {
                rotationMatrix[i, i] = 1;
            }
        }

        rotationMatrix[indexOne, indexTwo] = -sin;
        rotationMatrix[indexTwo, indexOne] = sin;

        return rotationMatrix;
    }
    
    private (int i, int j) GetMaxIndex(Matrix<double> matrix)
    {
        int maxI = -1;
        int maxJ = -1;
        
        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            for (int j = i; j < matrix.RowCount; j++)
            {
                if (i == j)
                    continue;

                if (maxI == -1 || Math.Abs(matrix[i, j]) > Math.Abs(matrix[maxI, maxJ]))
                {
                    maxI = i;
                    maxJ = j;
                }
            }
        }

        return (maxI, maxJ);
    }

    private double GetCos(int iOne, int iTwo, Matrix<double> matrix)
    {
        var tg = GetDoubleAngleTg(iOne, iTwo, matrix);

        if (double.IsPositiveInfinity(tg) || double.IsNegativeInfinity(tg)) 
            return Math.Sqrt(2) / 2;

        var doubleCos = 1 / Math.Sqrt(1 + tg * tg);
        return Math.Sqrt((1 + doubleCos) / 2);
    }
    
    private double GetSin(int iOne, int iTwo, Matrix<double> matrix)
    {
        var tg = GetDoubleAngleTg(iOne, iTwo, matrix);
        
        if (double.IsPositiveInfinity(tg))
            return Math.Sqrt(2) / 2;
        if (double.IsNegativeInfinity(tg))
            return -Math.Sqrt(2) / 2;
        
        var sign = matrix[iOne, iTwo] * (matrix[iOne, iOne] - matrix[iTwo, iTwo]) >= 0 ? 1 : -1;
        var doubleCos = 1 / Math.Sqrt(1 + tg * tg);
        return sign * Math.Sqrt((1 - doubleCos) / 2);
    }

    private double GetDoubleAngleTg(int iOne, int iTwo, Matrix<double> matrix)
    {
        if (Math.Abs(matrix[iOne, iOne] - matrix[iTwo, iTwo]) < 1e-9)
            return matrix[iOne, iTwo] > 0 ? Double.PositiveInfinity : Double.NegativeInfinity;

        return 2 * matrix[iOne, iTwo] / (matrix[iOne, iOne] - matrix[iTwo, iTwo]);
    }
}