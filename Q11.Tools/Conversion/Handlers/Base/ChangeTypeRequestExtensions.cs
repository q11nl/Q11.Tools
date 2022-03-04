using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base;

internal static class ChangeTypeRequestExtensions
{
    public static ChangeTypeResponse<T> GetResponse<T>(this ChangeTypeRequest<T> request, object? newValue)
    {
        var value = request.GetValue(newValue);
        return ChangeTypeResponse<T>.Create(value);
    }

    public static ChangeTypeResponse<T> GetResponse<T>(this ChangeTypeRequest<T> request, Type toDifferentType)
    {
        var value = request.value.ChangeType(toDifferentType, request.cultureInfo, request.returnDefaultValueWhenPossible);
        return ChangeTypeResponse<T>.Create((T?)value);
    }

    public static T? GetValue<T>(this ChangeTypeRequest<T> request, object? newValue)
    {
        return newValue.ChangeType<T>(request.cultureInfo, request.returnDefaultValueWhenPossible);
    }
}