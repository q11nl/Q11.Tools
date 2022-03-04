using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;
using Q11.Tools.Extensions;

namespace Q11.Tools.Conversion.Handlers;

internal class ConvertibleToNonEnumConvertibleHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.value is IConvertible && ConvertsWithIConvertible(request.toType) && !request.toType.IsEnum;
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        // This is the priginal .NET changetype
        return  (T)Convert.ChangeType(request.value, request.toType, request.cultureInfo);
    }

    private static bool ConvertsWithIConvertible(Type type)
    {
        return type.IsIn(typeof(bool), typeof(char), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort),
            typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double),
            typeof(decimal), typeof(DateTime), typeof(string));

    }
}