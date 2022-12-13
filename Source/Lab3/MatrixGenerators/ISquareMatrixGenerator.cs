using MathNet.Numerics.LinearAlgebra;

namespace Lab3.MatrixGenerators;

public interface ISquareMatrixGenerator<T> where T : struct, IEquatable<T>, IFormattable
{
    string Name { get; }
    Matrix<T> Generate(int size, int k);
}