using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientMethods;

public abstract class GradientMethod
{
    public abstract string Title { get; }
    public abstract string FullTitle { get; }
    
    public OptimizationResult FindMinimum(OptimizationRequest request)
    {
        List<Vector<double>> points = new List<Vector<double>>{request.StartPoint};
        var currentPoint = request.StartPoint;
        var currentFunctionValue = request.Function.Invoke(currentPoint);

        while (true)
        {
            var gradient = request.Function.GradientAt(currentPoint);
            
            if (gradient.Norm(gradient.Count) < request.GradientAccuracy)
                break;

            var newPoint = GetNextPoint(new NextPointFindParameters(request.Function, currentPoint));
            double newFunctionValue = request.Function.Invoke(newPoint);

            points.Add(newPoint);

            if ((newPoint - currentPoint).Norm(currentPoint.Count) < request.FunctionAccuracy 
                && Math.Abs(newFunctionValue - currentFunctionValue) < request.FunctionAccuracy) 
            {
                break;
            }
            
            currentPoint = newPoint;
            currentFunctionValue = newFunctionValue;
        }

        return new OptimizationResult(points.Last(), currentFunctionValue, points);
    }

    protected abstract Vector<double> GetNextPoint(NextPointFindParameters parameters);
}