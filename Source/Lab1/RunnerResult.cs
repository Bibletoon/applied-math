﻿using Lab1.OptimizationContexts;

namespace Lab1;

public record RunnerResult<T>(
    decimal Result, 
    int FunctionCallsCount, 
    int IterationsCount, 
    IReadOnlyList<T> Intervals) where T : IOptimizationContext;