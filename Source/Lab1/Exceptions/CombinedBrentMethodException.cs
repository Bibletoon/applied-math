using Lab1.OptimizationContexts;

namespace Lab1.Exceptions;

public class CombinedBrentMethodException : Exception
{
    public CombinedBrentMethodException(BrentOptimizationContext context, double u, double fu)
        : base($"Invalid brent method context: \tu: {u},\n\tfu: {fu}, \n\t{context}") { }
}