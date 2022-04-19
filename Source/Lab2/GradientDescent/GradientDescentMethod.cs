using Lab2.Models;
using Lab2.Tools;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientDescent;

public abstract class GradientDescentMethod
{
    public OptimizationResult FindMinimum(OptimizationRequest request)
    {
        List<Vector<double>> points = new List<Vector<double>>{request.StartPoint};
        var currentPoint = request.StartPoint;
        var currentFunctionValue = request.Function.Invoke(currentPoint);

        var lastPointSatisfy = false;

        while (true)
        {
            var gradient = request.Function.GradientAt(currentPoint);
            
            if (gradient.Norm(gradient.Count) < request.GradientAccuracy)
                break;

            var newPoint = GetNextPoint(new NextPointFindParameters(request.Function, currentPoint, gradient));
            double newFunctionValue = request.Function.Invoke(newPoint);

            points.Add(newPoint);

            // TODO: Think about this condition
            if ((newPoint - currentPoint).Norm(currentPoint.Count) < request.FunctionAccuracy 
                && Math.Abs(newFunctionValue - currentFunctionValue) < request.FunctionAccuracy) 
            {
                // TODO: do we need it for all methods?
                if (lastPointSatisfy)
                    break;

                lastPointSatisfy = true;
            } else lastPointSatisfy = false;
            
            currentPoint = newPoint;
            currentFunctionValue = newFunctionValue;
        }

        return new OptimizationResult(points.Last(), currentFunctionValue, points);
    }

    protected abstract Vector<double> GetNextPoint(NextPointFindParameters parameters);
}