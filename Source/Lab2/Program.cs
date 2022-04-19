﻿// See https://aka.ms/new-console-template for more information

using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab2;
using Lab2.GradientMethods;
using Lab2.Models;
using Lab2.Models.Functions;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;

var function = new ConstMathFunction((Vector<double> point) => 2 * point[0] * point[0] + point[0] * point[1] + point[1] * point[1], "2x^2+xy+y^2");

var startPoint = new DenseVector(new []{0.5, 1.0});
var task = new OptimizationRequest(startPoint, 1e-3, 1e-3, function);

var constStepGradient = new ConstStepGradientMethod(1);
var shrinkStepGradient = new ShrinkStepGradientMethod(1, 0.1, 0.75);

var fastGradient =
    new FastGradientMethod<GoldenRatioOptimisationContext>(new GoldenRatioMethod(),
                                                                  new GoldenRatioOptimisationContext(-10, 10), 1e-3);

var fletcherRives =
    new FletcherRivesMethod<GoldenRatioOptimisationContext>(new GoldenRatioMethod(),
                              new GoldenRatioOptimisationContext(-10, 10), 1e-3);
// TODO: refactor to lab1 style
var resultConst = constStepGradient.FindMinimum(task);
var resultShrink = shrinkStepGradient.FindMinimum(task);
var resultGradient = fastGradient.FindMinimum(task);
var resultFletcher = fletcherRives.FindMinimum(task);

var reportTasks = new List<OptimizationTask>()
{
    new OptimizationTask(constStepGradient.Title, task, resultConst),
    new OptimizationTask(shrinkStepGradient.Title, task, resultShrink),
    new OptimizationTask(fastGradient.Title, task, resultGradient),
    new OptimizationTask(fletcherRives.Title, task, resultFletcher)
};

var reporter = new GraphGenerator(reportTasks);

reporter.Generate("report");