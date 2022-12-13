using System;
using System.Collections.Generic;
using System.Linq;
using Lab3.EquationSystemSolvers;
using Lab3.Tools;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;
using NUnit.Framework;

namespace Lab3.Tests;

public class Tests
{
    private const int CaseCount = 10;

    private InverseMatrixCalculator _inverseMatrixCalculator = null!;

    [SetUp]
    public void Setup()
    {
        _inverseMatrixCalculator = new InverseMatrixCalculator(new GaussianEquationSystemSolver());
    }

    public static IEnumerable<Matrix> MatrixSource()
    {
        for (var i = 0; i < CaseCount; i++)
        {
            Matrix matrix;
            Matrix<double> l, u;

            do
            {
                matrix = NextMatrix();
                var res = LuFactorizator.Factorize(matrix);
                (l, u) = (res.L, res.U);
            }
            while (IsInvalid(l) || IsInvalid(u));

            yield return matrix;
        }
    }

    [Test]
    [TestCaseSource(nameof(MatrixSource))]
    public void LUTest_LUGenerated_LUMultiplicationEqualsInitialMatrix(Matrix matrix)
    {
        var result = LuFactorizator.Factorize(matrix);

        Console.WriteLine(matrix.ToString(matrix.RowCount, matrix.ColumnCount));
        Console.WriteLine((result.L * result.U).ToString(matrix.RowCount, matrix.ColumnCount));
        Console.WriteLine(result.L.ToString(matrix.RowCount, matrix.ColumnCount));
        Console.WriteLine(result.U.ToString(matrix.RowCount, matrix.ColumnCount));

        Assert.IsTrue(matrix.AlmostEqual(result.L * result.U, 3), "Incorrect LU decomposition.");
    }

    [Test]
    [TestCaseSource(nameof(MatrixSource))]
    public void InverseMatrixTest_InverseMatrixGenerated_MatricesMultiplicationEqualsIdentityMatrix(Matrix matrix)
    {
        Matrix<double> inversed = _inverseMatrixCalculator.Calculate(matrix);
        var identity = SparseMatrix.CreateIdentity(matrix.ColumnCount);

        Console.WriteLine(matrix.ToString(matrix.RowCount, matrix.ColumnCount));
        Console.WriteLine(inversed.ToString(matrix.RowCount, matrix.ColumnCount));
        Console.WriteLine((matrix * inversed).ToString(matrix.RowCount, matrix.ColumnCount));

        Assert.IsTrue(identity.AlmostEqual(matrix * inversed, 3), "Incorrect inverse matrix.");
    }

    private static Matrix NextMatrix()
    {
        var size = Random.Shared.Next(5, 20);
        var values = Enumerable.Range(0, size * size).Select(_ => NextDouble()).ToArray();
        var storage = SparseCompressedRowMatrixStorage<double>.OfInit(size, size, (i, j) => values[i * size + j]);
        return new SparseMatrix(storage);
    }

    private static double NextDouble()
        => Random.Shared.NextDouble() < 0.6 ? Random.Shared.Next() : 0;

    private static bool IsInvalid(Matrix<double> matrix)
    {
        return matrix
            .EnumerateRows()
            .SelectMany(r => r)
            .Any(x => double.IsInfinity(x) || double.IsNaN(x));
    }
}