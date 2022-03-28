﻿namespace Lab1.Tools;

public static class FibonacciCounter
{
    private static List<decimal> _cache = new List<decimal>(){1, 1};
    
    public static decimal GetNthNumber(int n)
    {
        if (n < 0)
            throw new ArgumentException("N must be positive or 0");

        while (_cache.Count <= n)
        {
            _cache.Add(_cache[^2] + _cache[^1]);
        }

        return _cache[n];
    }
}