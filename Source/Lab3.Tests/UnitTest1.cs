using System;
using System.Collections.Generic;
using System.Linq;
using Lab3.EquationSystemSolvers;
using Lab3.Tools;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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
            yield return NextMatrix();
        }
    }

    [Test]
    [TestCaseSource(nameof(MatrixSource))]
    public void LUTest_LUGenerated_LUMultiplicationEqualsInitialMatrix(Matrix matrix)
    {
        var result = LuFactorizator.Factorize(matrix);

        Console.WriteLine(result.L);
        Console.WriteLine(result.U);

        Assert.IsTrue(matrix.AlmostEqual(result.L * result.U, 3), "Incorrect LU decomposition.");
    }

    [Test]
    [TestCaseSource(nameof(MatrixSource))]
    public void InverseMatrixTest_InverseMatrixGenerated_MatricesMultiplicationEqualsIdentityMatrix(Matrix matrix)
    {
        Matrix<double> inversed = _inverseMatrixCalculator.Calculate(matrix);
        var identity = SparseMatrix.CreateIdentity(matrix.ColumnCount);
        
        Assert.IsTrue(identity.AlmostEqual(matrix * inversed, 3), "Incorrect inverse matrix.");
    }

    private static Matrix NextMatrix()
    {
        var size = Random.Shared.Next(5, 20);
        var storage = Enumerable.Range(0, size * size).Select(_ => NextDouble()).ToArray();
        return new DenseMatrix(size, size, storage);
    }

    private static double NextDouble()
        => Random.Shared.NextDouble() * Random.Shared.Next();
}