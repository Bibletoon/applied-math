namespace Lab1;

public class FunctionCallsCounter<TIn, TOut>
{
    private readonly Func<TIn, TIn> _accuracyModifier;
    private Func<TIn, TOut> _func;
    private Dictionary<TIn, TOut> _cache = new Dictionary<TIn, TOut>();

    public int CallsCount { get; private set; } = 0;

    public FunctionCallsCounter(Func<TIn, TOut> func, Func<TIn, TIn> accuracyModifier)
    {
        _func = func;
        _accuracyModifier = accuracyModifier;
    }

    public TOut Invoke(TIn arg)
    {
        arg = _accuracyModifier.Invoke(arg);
        if (!_cache.ContainsKey(arg))
        {
            _cache[arg] = _func.Invoke(arg);
            CallsCount++;
        }

        return _cache[arg];
    }
}