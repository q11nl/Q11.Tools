namespace Q11.Tools.Random;

internal static class ConvertExtensions
{
    public static T ConvertIConvertibleTo<TFrom, T>(this TFrom value) where TFrom : IConvertible => (T)Convert.ChangeType(value, typeof(T));

    public static T ConvertTo<TFrom, T>(this TFrom value) => (T)(object)value!;
}