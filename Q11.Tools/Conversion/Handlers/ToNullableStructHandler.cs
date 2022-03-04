using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class ToNullableStructHandler : HandlerBase
{
    public override ChangeTypeResponse<T> GetResponse<T>(ChangeTypeRequest<T> request)
    {
        if (request.ToTypeIsNullableStruct)
        {
            var toTypeNonNullable = Nullable.GetUnderlyingType(request.toType)!;

            if (request.value == null)
            {
                if (toTypeNonNullable.IsEnum || request.returnDefaultValueWhenPossible)
                {
                    return GetHandledResult<T>(default);
                }

                throw new InvalidCastException(
                    "Nullable struct type with null value can not be converted when value is null.");
            }

            if (toTypeNonNullable.IsEnum && request.valueIsString)
            {
                var value = (T)Enum.Parse(toTypeNonNullable, request.stringClean);
                return GetHandledResult(value);
            }

            return HandleAsNonNullableStruct();

            ChangeTypeResponse<T> HandleAsNonNullableStruct() => request.GetResponse(toTypeNonNullable);
        }

        return CanNotHandle<T>();
    }
}