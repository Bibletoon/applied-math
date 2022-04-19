using MathNet.Numerics.LinearAlgebra;

namespace Lab2.GradientDescent.StepFinders;

public interface IStepFinder
{
    double FindStepValue(Func<Vector<double>, double> func);
}