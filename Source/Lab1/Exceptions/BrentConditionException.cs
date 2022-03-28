namespace Lab1.Exceptions;

public class BrentConditionException : Exception
{
    public BrentConditionException(decimal fa, decimal fb, decimal fx, decimal x)
        : base($"Brent method conditions were not met: f(a) = {fa}, f(b) = {fb}, f(x) = {fx}, x = {x}") { }
}