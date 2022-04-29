using Lab2.GradientMethods;

namespace Lab2.Models;

public class OptimizationTask
{
    public string MethodName { get; init; }
    public string MethodFullName { get; init; }
    public OptimizationRequest Request { get; init; }
    public OptimizationResult Result { get; init; }

    public OptimizationTask(GradientMethod method, OptimizationRequest request, OptimizationResult result)
    {
        MethodName = method.Title;
        Request = request;
        Result = result;
        MethodFullName = method.FullTitle;
    }
}