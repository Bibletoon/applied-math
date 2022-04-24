using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientMethods;

public class ConstStepGradientMethod : GradientMethod
{
    private readonly double _startStepValue;
    private double _stepValue;

    public ConstStepGradientMethod(double startStepValue)
    {
        _startStepValue = startStepValue;
        _stepValue = startStepValue;
    }

    public override string Title => "Const Step Gradient Descent Method";
    public override string FullTitle => $"Const Step GDM with step = {_startStepValue}";

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        var currentValue = parameters.Function.Invoke(parameters.Point);
        while (true)
        {
            var newPoint = parameters.Point - _stepValue * parameters.Function.GradientAt(parameters.Point);
            var functionValue = parameters.Function.Invoke(newPoint);

            if (currentValue - functionValue >= 0)
            {
                return newPoint;
            }

            _stepValue /= 2;
        }
    }
}