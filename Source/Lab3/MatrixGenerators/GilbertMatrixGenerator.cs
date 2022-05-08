using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab3.MatrixGenerators;

public class GilbertMatrixGenerator : ISquareMatrixGenerator<double>
{
    public string Name => "Gilbert matrix";

    public Matrix<double> Generate(int size, int k)
    {
        Matrix<double> matrix = MatrixPool<double>.Get(k, k);

        for (var i = 0; i < k; i++)
        {
            for (var j = 0; j < k; j++)
            {
                matrix[i, j] = 1d / (i + j + 1);
            }
        }

        return matrix;
    }
}