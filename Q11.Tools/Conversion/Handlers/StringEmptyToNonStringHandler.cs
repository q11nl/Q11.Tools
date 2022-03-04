using Q11.Tools.Conversion.Handlers.Base;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers;

internal class StringEmptyToNonStringHandler : HandlerConditional
{
    public override bool CanHandle<T>(ChangeTypeRequest<T> request)
    {
        return request.stringClean == "" && !request.IsToType<string>();
    }

    public override T? GetValue<T>(ChangeTypeRequest<T> request) where T : default
    {
        if (request.returnDefaultValueWhenPossible && (request.toType.IsClass || request.ToTypeIsNullableStruct))
        {
            return default;
        }

        throw new FormatException("Empty string is not in a correct format for a struct.");
    }
}