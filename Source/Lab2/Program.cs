// See https://aka.ms/new-console-template for more information

using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab2.GradientMethods;
using Lab2.Models;
using Lab2.Models.Functions;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

var firstQFunc = new ConstMathFunction(
    f => (f[0] + 2 * f[1] - 7) * (f[0] + 2 * f[1] - 7) + (2 * f[0] + f[1] - 5) * (2 * f[0] + f[1] - 5),
    "(x+3∗y-12)^2+(2∗x+y+4)^2"
);

var secondQFunc = new ConstMathFunction(
    f => 2 * f[0] * f[0] + f[0] * f[1] + f[1] * f[1],
    "2x^2+xy+y^2"
);

var functionOptions = new List<FunctionOptimizationOptions>()
{
    new FunctionOptimizationOptions(
        firstQFunc,
        new []
        {
            new DenseVector(new []{-120.0,115.0}),
            new DenseVector(new []{1.0,1.0}),
            new DenseVector(new []{7.0, 5}),
        }),
    new FunctionOptimizationOptions(
        secondQFunc,
        new []
        {
            new DenseVector(new []{273.0, 25}),
            new DenseVector(new []{-1, 0.5}),
            new DenseVector(new []{2.0, 7}),
        }
        )
};

const double gradientAccuracy = 1e-3;
const double functionAccuracy = 1e-3;
const double oneDimMethodAccuracy = 1e-3;

const double LBorder = -100;
const double RBorder = 100;

var reporter = new ReportGenerator("Report");

foreach (var functionOption in functionOptions)
{
    var function = functionOption.Function;
    foreach (var startPoint in functionOption.StartPoints)
    {
        var task = new OptimizationRequest(startPoint, gradientAccuracy, functionAccuracy, function);

        var constStepGradient = new ConstStepGradientMethod(1);
        var shrinkStepGradient = new ShrinkStepGradientMethod(1, 0.1, 0.75);

        var fastGradient =
            new FastGradientMethod<GoldenRatioOptimisationContext>(new GoldenRatioMethod(),
                new GoldenRatioOptimisationContext(LBorder, RBorder), oneDimMethodAccuracy);

        var fletcherRives =
            new FletcherRivesMethod<GoldenRatioOptimisationContext>(new GoldenRatioMethod(),
                new GoldenRatioOptimisationContext(LBorder, RBorder), oneDimMethodAccuracy);

        var resultConst = constStepGradient.FindMinimum(task);
        var resultShrink = shrinkStepGradient.FindMinimum(task);
        var resultGradient = fastGradient.FindMinimum(task);
        var resultFletcher = fletcherRives.FindMinimum(task);

        var reportTasks = new List<OptimizationTask>()
        {
            new(constStepGradient.Title, constStepGradient.FullTitle, task, resultConst),
            new(shrinkStepGradient.Title, shrinkStepGradient.FullTitle, task, resultShrink),
            new(fastGradient.Title, fastGradient.FullTitle, task, resultGradient),
            new(fletcherRives.Title, fletcherRives.FullTitle, task, resultFletcher)
        };
        
        reporter.Generate(reportTasks);
    }
}