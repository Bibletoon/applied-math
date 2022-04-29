// See https://aka.ms/new-console-template for more information

using Lab1;
using Lab1.OptimisationMethods;
using Lab1.OptimizationContexts;
using Lab1.Tools;
using Lab2.GradientMethods;
using Lab2.Models;
using Lab2.Models.Functions;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra.Double;

public class Program
{
    private const double GradientAccuracy = 1e-3;
    private const double FunctionAccuracy = 1e-3;
    private const double OneDimMethodAccuracy = 1e-12;

    private const double LBorder = -100;
    private const double RBorder = 100;
    
    public static void Main(string[] args)
    {
        var firstQFunc = new ConstMathFunction(
            f => (f[0] + 2 * f[1] - 7) * (f[0] + 2 * f[1] - 7) + (2 * f[0] + f[1] - 5) * (2 * f[0] + f[1] - 5),
            "(x+3∗y-12)^2+(2∗x+y+4)^2"
        );

        var secondQFunc = new ConstMathFunction(
            f => (f[0] * f[0] + f[0] * f[1] + f[1] * f[1])/500,
            "(x^2+xy+y^2) div 500"
        );

        var thirdQFunc = new ConstMathFunction(
            f => (f[0] * f[0] + f[0] * f[1] + f[1] * f[1]) * 1000,
            "1000∗(x^2+xy+y^2)"
        );

        var lFun = new ConstMathFunction(
            f => Math.Log(f[0]*f[0]-f[0]*f[1]+3*f[1]*f[1]+3)+5,
            "log(x^2-xy+3y^2+3)+5"
        );

        var nuFun = new ConstMathFunction(
            f => Math.Pow(f[0] * f[0] + f[1] - 11, 2) + Math.Pow(f[0] + f[1] * f[1] - 7, 2),
            "(x^2 + y - 11)^2 + (x + y^2 -7)^2"
        );

        var functionOptions = new List<FunctionOptimizationOptions>()
        {
            new FunctionOptimizationOptions(
                firstQFunc,
                new[]
                {
                    new DenseVector(new[] { -120.0, 115.0 }),
                    new DenseVector(new[] { 1.0, 1.0 }),
                    new DenseVector(new[] { 7.0, 5 }),
                }),
            new FunctionOptimizationOptions(
                secondQFunc,
                new[]
                {
                    new DenseVector(new[] { 273.0, 25 }),
                    new DenseVector(new[] { -1, 0.5 }),
                    new DenseVector(new[] { 2.0, 7 }),
                }
            ),
            new FunctionOptimizationOptions(
                thirdQFunc,
                new[]
                {
                    new DenseVector(new[] { 273.0, 283 }),
                    new DenseVector(new[] { -1.0, 2 }),
                    new DenseVector(new[] { 12.0, 7 }),
                }
            ),
            new FunctionOptimizationOptions(
                lFun,
                new []
                {
                    new DenseVector(new []{10.0, 10}),
                    new DenseVector(new []{35.0, 72 }),
                    new DenseVector(new []{1.0, 2}),
                }
            ),
            new FunctionOptimizationOptions(
                nuFun,
                new []
                {
                    new DenseVector(new []{4, 3.5}),
                    new DenseVector(new []{-2.0, 4}),
                    new DenseVector(new []{-2, -4.2})
                }
                )
        };

        var reporter = new ReportGenerator("Report");

        foreach (var functionOption in functionOptions)
        {
            var function = functionOption.Function;
            foreach (var startPoint in functionOption.StartPoints)
            {
                var task = new OptimizationRequest(startPoint, GradientAccuracy, FunctionAccuracy, function);

                reporter.Generate(RunConstStepMethod(task));
                reporter.Generate(RunShrinkStepMethod(task));
                reporter.Generate(RunFletcherMethod(task));
                reporter.Generate(RunFastMethod(task));
            }
        }
    }

    private static List<OptimizationTask> RunConstStepMethod(OptimizationRequest request)
    {
        List<double> startStepValues = new List<double> { 1, 15, 125 };

        List<OptimizationTask> resultTasks = new List<OptimizationTask>();

        foreach (var stepValue in startStepValues)
        {
            var method = new ConstStepGradientMethod(stepValue);
            var result = method.FindMinimum(request);
            resultTasks.Add(new OptimizationTask(method, request, result));
        }

        return resultTasks;
    }

    private static List<OptimizationTask> RunShrinkStepMethod(OptimizationRequest request)
    {
        List<(double step, double shrink)> stepValues =
            new[] { 1.0, 15, 75 }.SelectMany(_ => new[] { 0.1, 0.95, 0.5 }, (st, sh) => (st, sh)).ToList();
        List<OptimizationTask> resultTasks = new List<OptimizationTask>();

        foreach (var (step, shrink) in stepValues)
        {
            var method = new ShrinkStepGradientMethod(step, 0.1, shrink);
            var result = method.FindMinimum(request);
            resultTasks.Add(new OptimizationTask(method, request, result));
        }

        return resultTasks;
    }

    private static List<OptimizationTask> RunFastMethod(OptimizationRequest request)
    {
        var goldenMethod = new GoldenRatioMethod();
        var fibonacciMethod = new FibonacciMethod();
        var n = GetN(LBorder, RBorder, OneDimMethodAccuracy);
        fibonacciMethod.N = n;

        var tasks = new List<OptimizationTask>();

        var fgm = new FastGradientMethod<GoldenRatioOptimisationContext>(
            goldenMethod,
            new GoldenRatioOptimisationContext(LBorder, RBorder),
            OneDimMethodAccuracy);

        // TODO: find way to pass N
        var ffm = new FastGradientMethod<FibonacciOptimizationContext>(
            fibonacciMethod,
            new FibonacciOptimizationContext(LBorder, RBorder),
            FunctionAccuracy
        );
        
        var result = fgm.FindMinimum(request);
        tasks.Add(new OptimizationTask(fgm, request, result));
        
        result = ffm.FindMinimum(request);
        tasks.Add(new OptimizationTask(ffm, request, result));

        return tasks;
    }
    
    private static int GetN(double a, double b, double accuracy)
    {
        var n = 1;

        while (FibonacciCounter.GetNthNumber(n) <= (b - a) / accuracy)
        {
            n++;
        }

        return n;
    }

    public static List<OptimizationTask> RunFletcherMethod(OptimizationRequest request)
    {
        var method = new GoldenRatioMethod();
        var tasks = new List<OptimizationTask>();
        var fletcherMethod = new FletcherRivesMethod<GoldenRatioOptimisationContext>(
            method,
            new GoldenRatioOptimisationContext(LBorder, RBorder),
            OneDimMethodAccuracy);

        var result = fletcherMethod.FindMinimum(request);
        tasks.Add(new OptimizationTask(fletcherMethod, request, result));

        return tasks;
    }
}



