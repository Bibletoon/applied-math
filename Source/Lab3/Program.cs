using Lab3.EquationSystemSolvers;
using Lab3.MatrixGenerators;
using Lab3.Tools;


const int n = 20;
const int resultCount = 5;
const double accuracy = 0.1;
const int maxIterationCount = 100;

var generator = new SpreadsheetGenerator();

var diagonallyDominantMatrixGenerator = new DiagonallyDominantMatrixGenerator();
var gilbertMatrixGenerator = new GilbertMatrixGenerator();
var randomMatrixGenerator = new RandomMatrixGenerator();

var jacobi = new JacobiEquationSystemSolver();
var seidel = new SeidelEquationSystemSolver();

var jacobiRunner = new SolutionRunner(jacobi, diagonallyDominantMatrixGenerator, maxIterationCount);
Loop(jacobiRunner, 0);
Console.WriteLine();

var seidelRunner = new SolutionRunner(seidel, diagonallyDominantMatrixGenerator, maxIterationCount);
Loop(seidelRunner, 1);
Console.WriteLine();

var gilbertJacobiRunner = new SolutionRunner(jacobi, gilbertMatrixGenerator, maxIterationCount);
Loop(gilbertJacobiRunner, 2, n, 4);
Console.WriteLine();

var gilbertSeidelRunner = new SolutionRunner(seidel, gilbertMatrixGenerator, maxIterationCount);
Loop(gilbertSeidelRunner, 3, n, 4);
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
    Loop(seidelRandomRunner, 0, size, neededCount: 1, showVectors: false);
    Loop(gaussianRunner, 2, size, neededCount: 1, showVectors: false);
    Loop(jacobiRandomRunner, 3, size, neededCount: 1, showVectors: false);

    Console.WriteLine();
}

generator.Build("Lab3.xlsx", "Direct/iterative comparison");

void Loop(
    SolutionRunner runner,
    int rowNumber,
    int size = n,
    int k = 0,
    int neededCount = resultCount,
    bool showVectors = true)
{
    var count = 0;

    while (count != neededCount)
    {
        var result = runner.Run(size, k, accuracy);
        generator.Enqueue(result, rowNumber, showVectors);
        count++;
        k++;
    }
}

int Pow(int i)
{
    return (int)Math.Pow(10, i);
}