using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientMethods;

public class ConstStepGradientMethod : GradientMethod
{
    private double _stepValue;

    public ConstStepGradientMethod(double startStepValue)
    {
        _stepValue = startStepValue;
    }

    public override string Title => "Const Step Gradient Descent Method";

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        var currentValue = parameters.Function.Invoke(parameters.Point);
        while (true)
        {
            var newPoint = parameters.Point - _stepValue * parameters.Function.GradientAt(parameters.Point);
            var functionValue = parameters.Function.Invoke(newPoint);

            if (currentValue - functionValue > 0)
            {
                return newPoint;
            }

            _stepValue /= 2;
        }
    }
}