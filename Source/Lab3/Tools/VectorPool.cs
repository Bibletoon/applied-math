using MathNet.Numerics.LinearAlgebra;

namespace Lab3.Tools;

public static class VectorPool<T> where T : struct, IEquatable<T>, IFormattable
{
    private static readonly Dictionary<int, List<Vector<T>>>
        Pool = new Dictionary<int, List<Vector<T>>>();

    public static Vector<T> Get(int length, Func<int, T>? init = null)
    {
        if (!Pool.TryGetValue(length, out List<Vector<T>>? list))
        {
            list = new List<Vector<T>>();
            Pool.Add(length, list);
        }

        if (list.Count is 0)
        {
            return init is null
                ? Vector<T>.Build.Dense(length)
                : Vector<T>.Build.Dense(length, init);
        }

        Vector<T> vector = list[0];

        if (init is not null)
        {
            for (var i = 0; i < vector.Count; i++)
            {
                vector[i] = init.Invoke(i);
            }
        }

        list.RemoveAt(0);

        return vector;
    }

    public static void Return(Vector<T> vector)
    {
        if (!Pool.TryGetValue(vector.Count, out List<Vector<T>>? list))
        {
            list = new List<Vector<T>>();
            Pool.Add(vector.Count, list);
        }

        vector.Clear();
        list.Add(vector);
    }
}