using Lab2.Models;
using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientDescent;

public class ShrinkStepGradientDescentMethod : GradientDescentMethod
{
    private readonly double _startStepValue;
    private readonly double _stepAccuracy;
    private readonly double _stepShrinkValue;

    public ShrinkStepGradientDescentMethod(double startStartStepValue, double stepAccuracy, double stepShrinkValue)
    {
        _startStepValue = startStartStepValue;
        _stepAccuracy = stepAccuracy;
        _stepShrinkValue = stepShrinkValue;
    }

    public override string Title => "Shrink Step Gradient Descent Method";

    protected override Vector<double> GetNextPoint(NextPointFindParameters parameters)
    {
        var currentValue = parameters.Function.Invoke(parameters.Point);
        var stepValue = _startStepValue;
        while (true)
        {
            var newPoint = parameters.Point - stepValue * parameters.Gradient;
            var functionValue = parameters.Function.Invoke(newPoint);

            if (functionValue <= currentValue - _stepAccuracy * stepValue * parameters.Gradient.Norm(parameters.Gradient.Count) * parameters.Gradient.Norm(parameters.Gradient.Count) )
            {
                return newPoint;
            }

            stepValue *= _stepShrinkValue;
        }
    }
}