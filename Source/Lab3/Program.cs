// See https://aka.ms/new-console-template for more information

using Lab3.EquationSystemSolvers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

Console.WriteLine("Hello, World!");

Matrix<double> matrix = new DenseMatrix(3, 3, new double[]
{
    2, 2, 3,
    4, 5, 6,
    7, 8, 9,
}).Transpose();

var result = new DenseVector(new double[] { 4, 7, 9 });

Vector<double> solution = new GaussianEquationSystemSolver().Solve(matrix, result);

Console.WriteLine(solution);