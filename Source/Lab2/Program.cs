// See https://aka.ms/new-console-template for more information

using Lab2;
using Lab2.GradientDescent;
using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

var function = (Vector<double> point) => 10 * point[0] * point[0] + point[1] * point[1];

var method = new ShrinkStepGradientDescentMethod(1, 0.1, 0.95);
var startPoint = new DenseVector(new []{10.0, 10.0});
var result = method.FindMinimum(new OptimizationTask(startPoint, 1e-5/2, 1e-5/2, function));
Console.WriteLine();