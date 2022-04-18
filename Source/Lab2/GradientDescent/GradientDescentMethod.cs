using Lab2.Models;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientDescent;

public abstract class GradientDescentMethod
{
    public OptimizationResult FindMinimum(OptimizationTask task)
    {
        List<Vector<double>> points = new List<Vector<double>>{task.StartPoint};
        var currentPoint = task.StartPoint;
        var currentFunctionValue = task.Function.Invoke(currentPoint);

        var lastPointSatisfy = false;

        while (true)
        {
            var gradient = task.Function.GradientAt(currentPoint);
            
            if (gradient.Norm(gradient.Count) < task.GradientAccuracy)
                break;

            var newPoint = GetNextPoint(new NextPointFindParameters(task.Function, currentPoint, currentPoint));
            double newFunctionValue = task.Function.Invoke(newPoint);

            points.Add(newPoint);

            if ((newPoint - currentPoint).Norm(currentPoint.Count) < task.FunctionAccuracy 
                && Math.Abs(newFunctionValue - currentFunctionValue) < task.FunctionAccuracy) 
            {
                if (lastPointSatisfy)
                    break;

                lastPointSatisfy = true;
            } else lastPointSatisfy = false;
            
            currentPoint = newPoint;
            currentFunctionValue = newFunctionValue;
        }

        return new OptimizationResult(points.Last(), currentFunctionValue, points);
    }

    //TODO: change arguments to one class
    protected abstract Vector<double> GetNextPoint(NextPointFindParameters parameters);
}