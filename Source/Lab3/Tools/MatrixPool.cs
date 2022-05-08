using MathNet.Numerics.LinearAlgebra;

namespace Lab3.Tools;

public static class MatrixPool<T> where T : struct, IEquatable<T>, IFormattable
{
    private static readonly Dictionary<(int, int), List<Matrix<T>>>
        Pool = new Dictionary<(int, int), List<Matrix<T>>>();

    public static Matrix<T> Get(int row, int column)
    {
        if (!Pool.TryGetValue((row, column), out List<Matrix<T>>? list))
        {
            list = new List<Matrix<T>>();
            Pool.Add((row, column), list);
        }

        if (list.Count is 0)
        {
            return Matrix<T>.Build.Dense(row, column);
        }

        Matrix<T> matrix = list[0];
        list.RemoveAt(0);

        return matrix;
    }

    public static void Return(Matrix<T> matrix)
    {
        var coordinates = (matrix.RowCount, matrix.ColumnCount);
        
        if (!Pool.TryGetValue(coordinates, out List<Matrix<T>>? list))
        {
            list = new List<Matrix<T>>();
            Pool.Add(coordinates, list);
        }
        
        matrix.Clear();
        list.Add(matrix);
    }
}