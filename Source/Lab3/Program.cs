using Lab3.EquationSystemSolvers;
using Lab3.MatrixGenerators;
using Lab3.Tools;
using MathNet.Numerics.LinearAlgebra;


const int n = 20;
const int gilbertK = 4;
const int resultCount = 5;
const double accuracy = 0.1;
const int maxIterationCount = 100;

var generator = new SpreadsheetGenerator();

var diagonallyDominantMatrixGenerator = new DiagonallyDominantMatrixGenerator();
var gilbertMatrixGenerator = new GilbertMatrixGenerator();

var jacobi = new JacobiEquationSystemSolver();
var seidel = new SeidelEquationSystemSolver();

var zeroNVector = VectorPool<double>.Get(n);

var jacobiRunner = new SolutionRunner(jacobi, diagonallyDominantMatrixGenerator, maxIterationCount);
Loop(jacobiRunner, (_, _) => zeroNVector, 0);
Console.WriteLine();

var seidelRunner = new SolutionRunner(seidel, diagonallyDominantMatrixGenerator, maxIterationCount);
Loop(seidelRunner, (_, _) => zeroNVector, 1);
Console.WriteLine();

var gilbertJacobiRunner = new SolutionRunner(jacobi, gilbertMatrixGenerator, maxIterationCount);
Loop(gilbertJacobiRunner, (_, k) => VectorPool<double>.Get(k), 2, k: gilbertK);
Console.WriteLine();

var gilbertSeidelRunner = new SolutionRunner(seidel, gilbertMatrixGenerator, maxIterationCount);
Loop(gilbertSeidelRunner, (_, k) => VectorPool<double>.Get(k), 3, k: gilbertK);
Console.WriteLine();

generator.Build("Lab3.xlsx", "Solution results");
generator.ClearQueue();

var gaussianIterative = new GaussianIterativeWrapper(new GaussianEquationSystemSolver());
var gaussianRunner = new SolutionRunner(gaussianIterative, diagonallyDominantMatrixGenerator, maxIterationCount);

var jacobiRandomRunner = new SolutionRunner(jacobi, diagonallyDominantMatrixGenerator, maxIterationCount);
var seidelRandomRunner = new SolutionRunner(seidel, diagonallyDominantMatrixGenerator, maxIterationCount);

var sizes = new[] { 10, 50, Pow(2), Pow(3) };

foreach (var size in sizes)
{
    var initialVector = VectorPool<double>.Get(size);
    Loop(seidelRandomRunner, (_, _) => initialVector, 0, size, neededCount: 1, showVectors: false);
    Loop(gaussianRunner, (_, _) => initialVector, 2, size, neededCount: 1, showVectors: false);
    Loop(jacobiRandomRunner, (_, _) => initialVector, 3, size, neededCount: 1, showVectors: false);

    Console.WriteLine();
}

generator.Build("Lab3.xlsx", "Direct/iterative comparison");

void Loop(
    SolutionRunner runner,
    // Функция, генерирующая вектор начального приближения
    Func<int, int, Vector<double>> initialApproximationFactory,
    // Номер строки в таблице, в которую будет записано результат
    int rowNumber,
    int size = n,
    int k = 0,
    // Количество необходимых решений (с инкрементом к)
    int neededCount = resultCount,
    bool showVectors = true)
{
    var count = 0;

    while (count != neededCount)
    {
        var result = runner.Run(size, k, accuracy, initialApproximationFactory.Invoke(n, k));
        generator.Enqueue(result, rowNumber, showVectors);
        count++;
        k++;
    }
}

int Pow(int i)
{
    return (int)Math.Pow(10, i);
}