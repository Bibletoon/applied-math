// See https://aka.ms/new-console-template for more information

using Lab2;
using Lab2.GradientDescent;
using Lab2.Models;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;

var function = (Vector<double> point) => 2 * point[0] * point[0] + point[0] * point[1] + point[1] * point[1];

var startPoint = new DenseVector(new []{0.5, 1.0});
var task = new OptimizationTask(startPoint, 1e-3, 1e-3, function);

var cnst = new ConstStepGradientDescentMethod(1);
var shrink = new ShrinkStepGradientDescentMethod(1, 0.1, 0.95);

var resultConst = cnst.FindMinimum(task);
var resultShrink = shrink.FindMinimum(task);

var model = GraphicGenerator.GenerateContourSeries(function, resultConst.PointsHistory.ToList());

using (var stream = File.Create("picture.svg"))
{
    SvgExporter.Export(model ,stream, 600, 600, false);
}