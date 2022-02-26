namespace Q11.Tools.Random.Handlers;

internal abstract class HandlerBase
{
    protected const int DaysIn60Years = 21915;

    protected GenericRandom GenericRandom { get; }

    protected HandlerBase(GenericRandom genericRandom)
    {
        GenericRandom = genericRandom;
    }

    public abstract bool CanHandle<T>();

    public abstract T GetValue<T>(int level);

    protected bool IsSameType<T1, T2>() => typeof(T1) == typeof(T2);
    protected bool IsInTypes<T>(params Type[] types) => types.Contains(typeof(T));

    internal int GetTnt(int min, int max) => GenericRandom.Random.Next(min, max);

    internal decimal GetDecimal(int min, int max)
    {
        const int decimals = 2;
        var factor = (int)Math.Pow(10, decimals);
        var intValue = GetTnt(min * factor, max * factor);
        return ((decimal)intValue) / factor;
    }

    internal char GetChar()
    {
        const string notConfusingCars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrtsuvwxyz23456789";
        return notConfusingCars[GenericRandom.Random.Next(notConfusingCars.Length)];
    }

    protected static T CreateObjectWithoutCallingConstructor<T>()
    {
        return (T)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(T));
    }
}