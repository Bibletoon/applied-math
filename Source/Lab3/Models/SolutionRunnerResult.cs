using MathNet.Numerics.LinearAlgebra;

namespace Lab3.Models;

public record struct SolutionRunnerResult(
    string MethodTitle,
    string MatrixTitle,
    Vector<double> InitialApproximation,
    Vector<double> Actual,
    Vector<double> Received,
    int K,
    double Accuracy,
    int IterationCount);