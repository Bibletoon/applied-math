using Lab1.OptimisationMethods;

namespace Lab1;

public static class Program
{
    public static void Main(string[] args)
    {
        var acc = Decimal.One/100;
        var delta = (decimal)0.003;

        var dih = new DichotomyMethod(delta);

        var res = OptimisationMethodRunner.FindFunctionMinimum(3*Decimal.MinusOne, 7*Decimal.One, acc, arg => arg * arg, dih);
    }
}