using Lab3.MatrixGenerators;
using Lab4;
using Lab4.MatrixGenerators;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public static class Program
{
    public static void Main(string[] args)
    {
        var sizes = new[] { 10, 25, 50 };
        var ks = new[] { 0, 1, 3, 5, 15 };
        using var reportFile = File.Open("report.md", FileMode.Create);
        using var reportWriter = new StreamWriter(reportFile);
        
        RunSolution(new SymmetricDiagonallyDominantMatrixGenerator(), sizes, ks, reportWriter);
        RunSolution(new GilbertMatrixGenerator(), new []{4}, new []{5, 10, 25, 50, 75, 100, 150, 250, 500}, reportWriter);
    }

    private static void RunSolution(ISquareMatrixGenerator<double> generator, int[] sizes, int[] kValues, StreamWriter reportWriter)
    {
        
        var method = new JacobiRotationMethod();
        
        foreach (var size in sizes)
        {
            foreach (var k in kValues)
            {
                var matrix = generator.Generate(size, k);

                var result = method.GetEigenvalues(matrix);
                reportWriter.WriteLine($"## {generator.Name}, size = {size}, k={k}\n");
                // reportWriter.WriteLine(MatrixToString(matrix));
                reportWriter.WriteLine("\n Found eigen values:\n");

                double acc = 0;
                foreach (var eigenValue in result.EigenValues)
                {
                    // reportWriter.WriteLine($"Number = {eigenValue.Number}\n");
                    // reportWriter.WriteLine($"Vector = ({string.Join("; ", eigenValue.Vector)})\n");

                    acc = Math.Max(CheckEigenValue(matrix, eigenValue), acc);
                }

                reportWriter.WriteLine($"Is right solution - {acc < 0.1}, accuracy - {acc}\n");
                reportWriter.WriteLine($"Iterations count - {result.IterationsCount}\n");
            }
        }
    }

    private static double CheckEigenValue(Matrix<double> matrix, EigenValue eigenValue)
    {
        var expected = eigenValue.Vector.Multiply(eigenValue.Number);
        var actual = matrix.Multiply(eigenValue.Vector);

        return CheckVectorsEquality(expected, actual);
    }

    private static double CheckVectorsEquality(Vector<double> expected, Vector<double> actual)
    {
        double acc = expected.Select((t, i) => Math.Abs(t - actual[i])).Prepend(0).Max();

        return acc;
    }

    private static string MatrixToString(Matrix<double> matrix)
    { 
        return string.Join("\n", matrix.EnumerateRows().Select(r => string.Join(" ", r)));
    }
}