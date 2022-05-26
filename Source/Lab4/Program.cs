using Lab3.MatrixGenerators;
using Lab4;
using Lab4.MatrixGenerators;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public static class Program
{
    public static void Main(string[] args)
    {
        var sizes = new[] { 10, 50, 100 };
        var ks = new[] { 4 };
        using var reportFile = File.Open("report.md", FileMode.Create);
        using var reportWriter = new StreamWriter(reportFile);
        RunSolution(new SymmetricDiagonallyDominantMatrixGenerator(), sizes, ks, reportWriter);
        RunSolution(new GilbertMatrixGenerator(), ks, sizes, reportWriter);
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
                reportWriter.WriteLine(MatrixToString(matrix));
                reportWriter.WriteLine("\n Found eigen values:\n");

                bool isRightSolution = true;
                foreach (var eigenValue in result.EigenValues)
                {
                    reportWriter.WriteLine($"Number = {eigenValue.Number}\n");
                    reportWriter.WriteLine($"Vector = ({string.Join("; ", eigenValue.Vector)})\n");

                    isRightSolution = CheckEigenValue(matrix, eigenValue);
                }

                reportWriter.WriteLine($"Is right solution - {isRightSolution}\n");
                reportWriter.WriteLine($"Iterations count - {result.IterationsCount}\n");
            }
        }
    }

    private static bool CheckEigenValue(Matrix<double> matrix, EigenValue eigenValue)
    {
        var expected = eigenValue.Vector.Multiply(eigenValue.Number);
        var actual = matrix.Multiply(eigenValue.Vector);

        return CheckVectorsEquality(expected, actual);
    }

    private static bool CheckVectorsEquality(Vector<double> expected, Vector<double> actual)
    {
        for (int i = 0; i < expected.Count; i++)
        {
            if (Math.Abs(expected[i] - actual[i]) >= 0.1)
            {
                return false;
            }
        }

        return true;
    }

    private static string MatrixToString(Matrix<double> matrix)
    { 
        return string.Join("\n", matrix.EnumerateRows().Select(r => string.Join(" ", r)));
    }
}