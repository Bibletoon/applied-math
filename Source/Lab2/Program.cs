// See https://aka.ms/new-console-template for more information

using Lab2;
using Lab2.GradientDescent;
using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

var function = (Vector<double> point) => 2 * point[0] * point[0] + point[0] * point[1] + point[1] * point[1];

var method = new ConstStepGradientDescentMethod(0.5);
var startPoint = new DenseVector(new []{0.5, 1});
var result = method.FindMinimum(new OptimizationTask(startPoint, 0.1, 0.15, function));
Console.WriteLine();