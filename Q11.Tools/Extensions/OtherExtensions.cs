namespace Q11.Tools.Extensions;

public static class OtherExtensions
{
    public static bool IsIn<T>(this T source, params T[]? values)
    {
        if (source == null || values == null)
        {
            return false;
        }
        return values.Any(v => source.Equals(v));
    }
}