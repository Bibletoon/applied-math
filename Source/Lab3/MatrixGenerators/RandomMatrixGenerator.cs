using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab3.MatrixGenerators;

public class RandomMatrixGenerator : ISquareMatrixGenerator<double>
{
    public string Name => "Random matrix";

    public Matrix<double> Generate(int size, int k)
    {
        Matrix<double> matrix = MatrixPool<double>.Get(size, size);

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                matrix[i, j] = Random.Shared.Next();
            }
        }

        return matrix;
    }
}