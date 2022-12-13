using Lab3.MatrixGenerators;
using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab4.MatrixGenerators;

public class SymmetricDiagonallyDominantMatrixGenerator : ISquareMatrixGenerator<double>
{
    public string Name => "Symmetric diagonally dominant matrix";

    public Matrix<double> Generate(int size, int k)
    {
        var matrix = MatrixPool<double>.Get(size, size);
        var sum = 0d;

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                if (i == j)
                    continue;

                matrix[i, j] = i > j ? matrix[j, i] : NextInt(4);
                sum += matrix[i, j];
            }
        }

        for (var i = 0; i < size; i++)
        {
            matrix[i, i] = -sum + Math.Pow(10, k);
        }

        return matrix;
    }
    
    private static int NextInt(int maxValue)
    {
        return -Random.Shared.Next(maxValue);
    }
}