using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientDescent;

public class ConstStepGradientDescentMethod : GradientDescentMethod
{
    private double _stepValue;

    public ConstStepGradientDescentMethod(double startStepValue)
    {
        _stepValue = startStepValue;
    }

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        var currentValue = parameters.Function.Invoke(parameters.Point);
        while (true)
        {
            var newPoint = parameters.Point - _stepValue * parameters.Gradient;
            var functionValue = parameters.Function.Invoke(newPoint);

            if (currentValue - functionValue > 0)
            {
                return newPoint;
            }

            _stepValue /= 2;
        }
    }
}