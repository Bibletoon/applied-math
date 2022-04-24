namespace Lab2.Models;

public class OptimizationTask
{
    public string MethodName { get; init; }
    public string MethodFullName { get; init; }
    public OptimizationRequest Request { get; init; }
    public OptimizationResult Result { get; init; }

    public OptimizationTask(string methodName, string methodFullName, OptimizationRequest request, OptimizationResult result)
    {
        MethodName = methodName;
        Request = request;
        Result = result;
        MethodFullName = methodFullName;
    }
}