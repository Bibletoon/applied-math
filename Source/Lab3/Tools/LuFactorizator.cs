using Lab3.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab3.Tools;

public static class LuFactorizator
{
    public static LuDecompositionResult Factorize(Matrix<double> matrix)
    {
        var l = new SparseMatrix(matrix.RowCount, matrix.ColumnCount);
        var u = new SparseMatrix(matrix.RowCount, matrix.ColumnCount);
        var n = matrix.RowCount;

        for (var i = 0; i < n; i++)
        {
            l[i, i] = 1;
        }

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i <= j)
                {
                    var sum = 0d;

                    for (var k = 0; k < i; k++)
                        sum += l[i, k] * u[k, j];

                    u[i, j] = matrix[i, j] - sum;
                }
                else if (i > j)
                {
                    var sum = 0d;

                    for (var k = 0; k < j; k++)
                        sum += l[i, k] * u[k, j];

                    l[i, j] = (matrix[i, j] - sum) / u[j, j];
                }
            }
        }

        return new LuDecompositionResult(l, u);
    }
}