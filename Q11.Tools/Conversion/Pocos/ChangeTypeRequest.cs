using System.Globalization;

namespace Q11.Tools.Conversion.Pocos;

internal class ChangeTypeRequest<T>
{
    public object? value { get; }
    public CultureInfo cultureInfo { get; }
    public bool returnDefaultValueWhenPossible { get; }
    public string stringClean { get; }
    public bool valueIsString { get; }
    public Type fromType { get; }
    public Type toType { get; }
    
    public T? ValueCastedToT => value is { } nonNullValue ? (T)nonNullValue : default;
    public bool IsFromImplementing<TOther>() => value is TOther;
    public bool IsToImplementing<TOther>() => toType.IsAssignableFrom(typeof(TOther));

    public bool ToTypeIsNullableStruct => toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>);
    public bool IsFromType<TFromType>() => fromType == typeof(TFromType);
    public bool IsToType<TToType>() => toType == typeof(TToType);
    public bool IsFromToType<TFromType,TToType>() => IsFromType<TFromType>() && IsToType<TToType>();

    public ChangeTypeRequest(object? value, CultureInfo cultureInfo, bool returnDefaultValueWhenPossible)
    {
        this.value = value;
        this.cultureInfo = cultureInfo;
        this.returnDefaultValueWhenPossible = returnDefaultValueWhenPossible;
        stringClean = (value as string ?? "").Trim();
        fromType = value?.GetType() ?? typeof(object);
        toType = typeof(T);
        valueIsString = fromType == typeof(string);

    }
}