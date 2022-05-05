using MathNet.Numerics.LinearAlgebra;

namespace Lab3.Models;

public record struct LuDecompositionResult(Matrix<double> L, Matrix<double> U);