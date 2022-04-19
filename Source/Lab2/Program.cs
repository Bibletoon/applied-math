// See https://aka.ms/new-console-template for more information

using Lab2;
using Lab2.GradientDescent;
using Lab2.Models;
using Lab2.Models.Functions;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;

var function = new ConstMathFunction((Vector<double> point) => 2 * point[0] * point[0] + point[0] * point[1] + point[1] * point[1], "2x^2+xy+y^2");

var startPoint = new DenseVector(new []{0.5, 1.0});
var task = new OptimizationRequest(startPoint, 1e-3, 1e-3, function);

var cnst = new ConstStepGradientDescentMethod(1);
var shrink = new ShrinkStepGradientDescentMethod(1, 0.1, 0.75);

var resultConst = cnst.FindMinimum(task);
var resultShrink = shrink.FindMinimum(task);

var reportTasks = new List<OptimizationTask>()
{
    new OptimizationTask("Const Step Gradient", task, resultConst),
    new OptimizationTask("Shrink Step Gradient", task, resultShrink)
};

var reporter = new GraphGenerator(reportTasks);

reporter.Generate("report");